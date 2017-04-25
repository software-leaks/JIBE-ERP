<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERLogDetailsWatchHours.aspx.cs"
    Inherits="Technical_ERLog_ERLogDetailsWatchHours" Title="ER Log Book - Running 6 Watches"
    MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <title>Engine Log Book Details</title>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Deck_Engine_LogBook.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
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
        .doublescroll
        {
            overflow: hidden;
            overflow-x: scroll;
        }
        .doublescroll p
        {
            margin: 0;
            padding: 1em;
            white-space: nowrap;
        }
    </style>
    <script type="text/javascript">
        window.onload = function () {
            function DoubleScroll(element) {
                var scrollbar = document.createElement('div');
                scrollbar.appendChild(document.createElement('div'));
                scrollbar.style.overflow = 'auto';
                scrollbar.style.overflowY = 'hidden';
                scrollbar.firstChild.style.width = element.scrollWidth + 20 + 'px';
                scrollbar.firstChild.style.paddingTop = '1px';
                scrollbar.firstChild.appendChild(document.createTextNode('\xA0'));
                scrollbar.onscroll = function () {
                    element.scrollLeft = scrollbar.scrollLeft;
                };
                element.onscroll = function () {
                    scrollbar.scrollLeft = element.scrollLeft;
                };
                element.parentNode.insertBefore(scrollbar, element);
            }
            DoubleScroll(document.getElementById('dvPageContent'));

        }
    </script>
    <style type="text/css">
        .page
        {
            size: landscape;
        }
        
        
        
        .CreateHtmlTableFromDataTable-table
        {
            background-color: #FFFFFF;
            border: 1px solid #FFB733;
        }
        
        .CreateHtmlTableFromDataTable-PageHeader
        {
            background-color: #F6B680;
        }
        
        .CreateHtmlTableFromDataTable-DataHedaer
        {
            background-color: #F2F2F2;
            border: 1px solid gray;
            text-align: center;
        }
        .CreateHtmlTableFromDataTable-Data
        {
            border: 1px solid gray;
        }
        .CellClass1
        {
            background-color: #FA5858;
            color: White;
        }
        .CellClass0
        {
        }
        .HeaderCellColor
        {
            background-color: #3399CC;
            color: White;
        }
        .HeaderCellColor1
        {
            background-color: #BCF5A9;
            color: Black;
        }
        .CurCell
        {
            background-color: #99FF00;
        }
        
        
        border-right:1px solid #cccccc;</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
 
    <div style="border: 1px solid #cccccc; width: 95%; height: 100%;">
  
            <script type="text/javascript">



                function printDiv(divID) {
                    //Get the HTML of div
                    var divElements = document.getElementById(divID).innerHTML;
                    //Get the HTML of whole page
                    var oldPage = document.body.innerHTML;

                    //Reset the page's HTML with div's HTML only
                    document.body.innerHTML =
              "<html><head><title></title></head><body>" +
              divElements + "</body>";

                    //Print Page
                    window.print();

                    //Restore orignal HTML
                    document.body.innerHTML = oldPage;


                }


 
    </script>
              <asp:UpdateProgress ID="upUpdateProgress" runat="server">
                <ProgressTemplate>
                    <div id="blur-on-updateprogress">
                        &nbsp;</div>
                    <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                        color: black">
                        <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UP1" runat="server">
            <ContentTemplate>
              <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                height: 100%;">
                <div id="page-header" class="page-title">
                    <b>ENGINE ROOM LOG BOOK - Running 6 Watches</b>
                </div>
                 
                <br />
                <table cellspacing="0" cellpadding="3"  border="0"  style="background-color: White; padding:0px 0px 7px 0px;
                    width: 100%; border:0;">
                    <tr>
                        <td align="left">
                            Fleet
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="DDLFleet" runat="server" UseInHeader="false" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged"
                                AutoPostBack="true" Width="160" />
                        </td>
                        <td align="left">
                            Date From
                        </td>
                        <td>
                            <asp:TextBox ID="txtFromDate" CssClass="input" runat="server"    AutoPostBack="true"
                                  ontextchanged="txtFromDate_TextChanged"></asp:TextBox>
                            <cc1:CalendarExtender ID="calFromDate" runat="server" Enabled="True" TargetControlID="txtFromDate"
                                Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                        </td>
                      
                    <td rowspan="2" style="border:1px solid black;width:50px">
                    <asp:ImageButton ID="btnPrevHour" runat="server" ImageUrl="~/Images/panel-prev-big.png" 
                                 onclick="btnPrevHour_Click" />
                    </td>
                 
                    <td rowspan="2" align="center" style="background-color:#FFD8B0">
                     <asp:DataList  ID="rpSilider" runat="server" RepeatDirection="Horizontal"  style="width:100%"
                            RepeatColumns="6" GridLines="Both" onitemdatabound="rpSilider_ItemDataBound">
                     <ItemTemplate>
                     
                     <asp:Label ID="lblDateSlider" runat="server" Text='<%# Convert.ToDateTime(Eval("WatchDate")).ToString("dd/MMM/yyyy") %>'  style="padding:15px 0px 0px 10px"  ></asp:Label>
                     <hr>
                    
                     <asp:Label ID="lblDateWach" runat="server"  Text='<%# Eval("WatchHours").ToString() %>'   style="padding:0px 0px 10px 10px;font-weight:bold" ></asp:Label>
                    
                     </ItemTemplate>
                     

                     </asp:DataList>
                    </td>
                    <td rowspan="2" style="border:1px solid black;width:50px">
                    <asp:ImageButton ID="btnNextHour" runat="server" 
                            ImageUrl="~/Images/panel-next-big.png" onclick="btnNextHour_Click" />
                    </td>
                    <td rowspan="2">
                    </td>
                         <td align="left">
                            Date To
                        </td>
                        <td>
                            <asp:TextBox ID="txtDateTo" CssClass="input" runat="server"    AutoPostBack="true"
                                  ontextchanged="txtDateTo_TextChanged"  ></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtDateTo"
                                Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Vessels
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="DDLVessel" runat="server" UseInHeader="false" OnSelectedIndexChanged="DDLVessel_SelectedIndexChanged"
                                AutoPostBack="true" Width="160" />
                        </td>
                        <td align="left">
                           Watch From
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlWatchHours" runat="server"  AutoPostBack="true"
                                onselectedindexchanged="ddlWatchHours_SelectedIndexChanged">
                                <asp:ListItem Text="0000-0400" Value="1" Selected="True" />
                                <asp:ListItem Text="0400-0800" Value="2" />
                                <asp:ListItem Text="0800-1200" Value="3" />
                                <asp:ListItem Text="1200-1600" Value="4" />
                                <asp:ListItem Text="1600-2000" Value="5" />
                                <asp:ListItem Text="2000-2400" Value="6" />
                            </asp:DropDownList>
                        </td>
                     <td align="left">
                           Watch To
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlWatchHourT" runat="server"  AutoPostBack="true" onselectedindexchanged="ddlWatchHourT_SelectedIndexChanged"
                               >
                                <asp:ListItem Text="0000-0400" Value="1" Selected="True" />
                                <asp:ListItem Text="0400-0800" Value="2" />
                                <asp:ListItem Text="0800-1200" Value="3" />
                                <asp:ListItem Text="1200-1600" Value="4" />
                                <asp:ListItem Text="1600-2000" Value="5" />
                                <asp:ListItem Text="2000-2400" Value="6" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    
                </table>
            </div>

            <div style="text-align: left; overflow: scroll;font-size:small" id="dvPageContent" class="page-content-main doublescroll">
                <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                    border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                    <tr>
                        <td style="width: 100%; border-right: solid; border-right-color: Gray; border-right-width: 1px"
                            align="left" valign="top">
<%--                            <asp:FormView ID="FormView1" runat="server" Width="100%" BorderWidth="0px" Font-Size="Small">
                                <RowStyle CssClass="PMSGridRowStyle-css" Height="18" />
                                <ItemTemplate>--%>
                                    <table cellspacing="0" cellpadding="3" border="0.5" style="background-color: White;
                                        border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;Font-Size:small">
                                        <tr>
                                            <td width="100%" colspan="3" valign="top">
                                                <asp:Repeater ID="rpEngine1" runat="server">
                                                    <HeaderTemplate>
                                                        <table cellspacing="1" cellpadding="3" rules="all" border="0" style="background-color: White; white-space:nowrap;
                                                            border-color: #efefef; width: 100%;">
                                                            <tr align="center" class="HeaderCellColor1">
                                                                <td colspan="22" align="center">
                                                                    MAIN ENGINES
                                                                </td>
                                                                <td colspan="37" align="center">
                                                                    MAIN ENGINE TURBO BLOWERS
                                                                </td>
                                                            </tr>
                                                            <tr align="center" class="HeaderCellColor">
                                                                <td rowspan="4">
                                                                    Watch
                                                                </td>
                                                                <td rowspan="4">
                                                                    <label id="Label176" class="verticaltext">
                                                                        Hours
                                                                    </label>
                                                                </td>
                                                                <td rowspan="4">
                                                                    <label id="lbl1" class="verticaltext">
                                                                        minutes
                                                                    </label>
                                                                </td>
                                                                <td rowspan="3" colspan="2">
                                                                    Revolutions
                                                                </td>
                                                                <td rowspan="4">
                                                                    <label id="Label177" class="verticaltext">
                                                                        ME control</label>
                                                                </td>
                                                                <td rowspan="4">
                                                                    <label id="Label178" class="verticaltext">
                                                                        Gov control</label>
                                                                </td>
                                                                <td rowspan="4">
                                                                    <label id="Label179" class="verticaltext">
                                                                        Load indicator</label>
                                                                </td>
                                                                <td rowspan="4">
                                                                    <label id="Label185" class="verticaltext">
                                                                        Fuel PP INDEX</label>
                                                                </td>
                                                                <td rowspan="4">
                                                                    <label id="Label186" class="verticaltext">
                                                                        Fuel FLOWMETER</label>
                                                                </td>
                                                                <td rowspan="2" colspan="12">
                                                                    TEMPERATURE C
                                                                </td>
                                                                <td rowspan="3" colspan="3">
                                                                    T/CHGR RPM X 1000
                                                                </td>
                                                                <td colspan="23">
                                                                    TEMPERATURE C
                                                                </td>
                                                                <td colspan="23">
                                                                    PRESSURE
                                                                </td>
                                                            </tr>
                                                            <tr align="center" class="HeaderCellColor">
                                                                <td colspan="6">
                                                                    EXHAUST GAS
                                                                </td>
                                                                <td colspan="6">
                                                                    AIR COOLER AIR
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label187" class="verticaltext1">
                                                                        Scavange</label>
                                                                </td>
                                                                <td colspan="4">
                                                                    COOLING WATER
                                                                </td>
                                                                <td colspan="6">
                                                                    LUB OIL
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label189" class="verticaltext1">
                                                                        Scav km/cm2</label>
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label188" class="verticaltext1">
                                                                        EXN BACK
                                                                    </label>
                                                                </td>
                                                                <td colspan="6">
                                                                    Press Drop mm Wc
                                                                </td>
                                                            </tr>
                                                            <tr align="center" class="HeaderCellColor">
                                                                <td colspan="12">
                                                                    EXHAUST
                                                                </td>
                                                                <td colspan="3">
                                                                    INLET
                                                                </td>
                                                                <td colspan="3">
                                                                    OUTLET
                                                                </td>
                                                                <td colspan="2">
                                                                    1
                                                                </td>
                                                                <td colspan="2">
                                                                    2
                                                                </td>
                                                                <td colspan="2">
                                                                    3
                                                                </td>
                                                                <td>
                                                                    IN
                                                                </td>
                                                                <td colspan="3">
                                                                    OUTLET
                                                                </td>
                                                                <td colspan="2">
                                                                    1
                                                                </td>
                                                                <td colspan="2">
                                                                    2
                                                                </td>
                                                                <td colspan="2">
                                                                    3
                                                                </td>
                                                                <td colspan="3">
                                                                    AIR COOLER
                                                                </td>
                                                                <td colspan="3">
                                                                    AIR FILTER
                                                                </td>
                                                            </tr>
                                                            <tr align="center" class="HeaderCellColor">
                                                                <td>
                                                                    COUNTER
                                                                </td>
                                                                <td>
                                                                    RPM
                                                                </td>
                                                                <td width="40px">
                                                                    1
                                                                </td>
                                                                <td width="40px">
                                                                    2
                                                                </td>
                                                                <td width="40px">
                                                                    3
                                                                </td>
                                                                <td width="40px">
                                                                    4
                                                                </td>
                                                                <td width="40px">
                                                                    5
                                                                </td>
                                                                <td width="40px">
                                                                    6
                                                                </td>
                                                                <td width="40px">
                                                                    7
                                                                </td>
                                                                <td width="40px">
                                                                    8
                                                                </td>
                                                                <td width="40px">
                                                                    9
                                                                </td>
                                                                <td width="40px">
                                                                    10
                                                                </td>
                                                                <td width="40px">
                                                                    11
                                                                </td>
                                                                <td width="40px">
                                                                    12
                                                                </td>
                                                                <td width="40px">
                                                                    1
                                                                </td>
                                                                <td width="40px">
                                                                    2
                                                                </td>
                                                                <td width="40px">
                                                                    3
                                                                </td>
                                                                <td width="40px">
                                                                    1
                                                                </td>
                                                                <td width="40px">
                                                                    2
                                                                </td>
                                                                <td width="40px">
                                                                    3
                                                                </td>
                                                                <td width="40px">
                                                                    1
                                                                </td>
                                                                <td width="40px">
                                                                    2
                                                                </td>
                                                                <td width="40px">
                                                                    3
                                                                </td>
                                                                <td width="40px">
                                                                    IN
                                                                </td>
                                                                <td width="40px">
                                                                    OUT
                                                                </td>
                                                                <td width="40px">
                                                                    IN
                                                                </td>
                                                                <td width="40px">
                                                                    OUT
                                                                </td>
                                                                <td width="40px">
                                                                    IN
                                                                </td>
                                                                <td width="40px">
                                                                    OUT
                                                                </td>
                                                                <td width="40px">
                                                                    ..
                                                                </td>
                                                                <td width="40px">
                                                                    1
                                                                </td>
                                                                <td width="40px">
                                                                    2
                                                                </td>
                                                                <td width="40px">
                                                                    3
                                                                </td>
                                                                <td width="40px">
                                                                    B
                                                                </td>
                                                                <td width="40px">
                                                                    T
                                                                </td>
                                                                <td width="40px">
                                                                    B
                                                                </td>
                                                                <td width="40px">
                                                                    T
                                                                </td>
                                                                <td width="40px">
                                                                    B
                                                                </td>
                                                                <td width="40px">
                                                                    T
                                                                </td>
                                                                <td width="40px">
                                                                    1
                                                                </td>
                                                                <td width="40px">
                                                                    2
                                                                </td>
                                                                <td width="40px">
                                                                    3
                                                                </td>
                                                                <td width="40px">
                                                                    1
                                                                </td>
                                                                <td width="40px">
                                                                    2
                                                                </td>
                                                                <td width="40px">
                                                                    3
                                                                </td>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr align="center">
                                                            <td align="left" style="height: 19px;">
                                                                <asp:Label ID="Label159m" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LOG_WATCH")%>'
                                                                    Width="60"> </asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label235" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WATCH_HOURS")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label236" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WATCH_MINUTES")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_COUNTER")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_RPM")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_CONTROL")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_GOV_CTRL")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_LOAD_INDICATOR")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label160" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_FUEL_PP_INDEX")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label161" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_FUEL_FLOWMETER")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_01_Color").ToString() %>'>
                                                                <asp:Label ID="Label21" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_01")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_02_Color").ToString()  %>'>
                                                                <asp:Label ID="Label22" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_02")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_03_Color").ToString()  %>'>
                                                                <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_03")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_04_Color").ToString()  %>'>
                                                                <asp:Label ID="Label168" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_04")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_05_Color").ToString()  %>'>
                                                                <asp:Label ID="Label23" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_05")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_06_Color").ToString()  %>'>
                                                                <asp:Label ID="Label24" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_06")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_07_Color").ToString()  %>'>
                                                                <asp:Label ID="Label25" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_07")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_08_Color").ToString()  %>'>
                                                                <asp:Label ID="Label28" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_08")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_09_Color").ToString()  %>'>
                                                                <asp:Label ID="Label30" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_09")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_10_Color").ToString()  %>'>
                                                                <asp:Label ID="Label34" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_10")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_11_Color").ToString()  %>'>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_11")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_12_Color").ToString()  %>'>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_12")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TC_RPM_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TC_RPM_2")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label12" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TC_RPM_3")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_EXH_IN_1_Color").ToString()  %>'>
                                                                <asp:Label ID="Label13" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_EXH_IN_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_EXH_IN_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label14" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_EXH_IN_2")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_EXH_IN_3_Color").ToString()  %>'>
                                                                <asp:Label ID="Label15" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_EXH_IN_3")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_EXH_OUT_1_Color").ToString()  %>'>
                                                                <asp:Label ID="Label16" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_EXH_OUT_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_EXH_OUT_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label17" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_EXH_OUT_2")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_EXH_OUT_3_Color").ToString()  %>'>
                                                                <asp:Label ID="Label18" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_EXH_OUT_3")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_AIR_IN_1_Color").ToString()  %>'>
                                                                <asp:Label ID="Label19" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_AIR_IN_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_AIR_OUT_1_Color").ToString()  %>'>
                                                                <asp:Label ID="Label20" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_AIR_OUT_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_AIR_in_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label26" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_AIR_IN_2")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_AIR_OUT_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label27" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_AIR_OUT_2")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_AIR_IN_3_Color").ToString()  %>'>
                                                                <asp:Label ID="Label29" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_AIR_IN_3")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_AIR_OUT_3_Color").ToString()  %>'>
                                                                <asp:Label ID="Label31" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_AIR_OUT_3")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_SCAVENGE_Color").ToString()  %>'>
                                                                <asp:Label ID="Label32" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_SCAVENGE")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label33" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_CW_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label35" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_CW_OUT_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label36" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_CW_OUT_2")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label37" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_CW_OUT_3")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_LO_B_1_Color").ToString()  %>'>
                                                                <asp:Label ID="Label38" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_LO_B_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_LO_T_1_Color").ToString()  %>'>
                                                                <asp:Label ID="Label39" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_LO_T_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_LO_B_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label42" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_LO_B_2")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_LO_T_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label43" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_LO_T_2")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_LO_B_3_Color").ToString()  %>'>
                                                                <asp:Label ID="Label44" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_LO_B_3")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_LO_T_3_Color").ToString()  %>'>
                                                                <asp:Label ID="Label45" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_LO_T_3")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label40" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_SCAVENGE_KGPCMS")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label41" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_EXH_BACK")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"P_PD_AC_1_Color").ToString()  %>'>
                                                                <asp:Label ID="Label46" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_PD_AC_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"P_PD_AC_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label47" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_PD_AC_2")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"P_PD_AC_3_Color").ToString()  %>'>
                                                                <asp:Label ID="Label48" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_PD_AC_3")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label49" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_PD_AF_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label50" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_PD_AF_2")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label51" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_PD_AF_3")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr align="center" style="background-color: #efefcf; border: 1px solid ActiveBorder">
                                                            <td align="left" style="height: 19px;">
                                                                <asp:Label ID="Label159a" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LOG_WATCH")%>'> </asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label235" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WATCH_HOURS")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label236" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WATCH_MINUTES")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_COUNTER")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_RPM")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_CONTROL")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_GOV_CTRL")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_LOAD_INDICATOR")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label160" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_FUEL_PP_INDEX")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label161" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_FUEL_FLOWMETER")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_01_Color").ToString()  %>'>
                                                                <asp:Label ID="Label21" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_01")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_02_Color").ToString()  %>'>
                                                                <asp:Label ID="Label22" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_02")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_03_Color").ToString()  %>'>
                                                                <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_03")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_04_Color").ToString()  %>'>
                                                                <asp:Label ID="Label168" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_04")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_05_Color").ToString()  %>'>
                                                                <asp:Label ID="Label23" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_05")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_06_Color").ToString()  %>'>
                                                                <asp:Label ID="Label24" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_06")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_07_Color").ToString()  %>'>
                                                                <asp:Label ID="Label25" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_07")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_08_Color").ToString()  %>'>
                                                                <asp:Label ID="Label28" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_08")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_09_Color").ToString()  %>'>
                                                                <asp:Label ID="Label30" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_09")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_10_Color").ToString()  %>'>
                                                                <asp:Label ID="Label34" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_10")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_11_Color").ToString()  %>'>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_11")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"ME_EXH_TEMP_12_Color").ToString()  %>'>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_12")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TC_RPM_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TC_RPM_2")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label12" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TC_RPM_3")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_EXH_IN_1_Color").ToString()  %>'>
                                                                <asp:Label ID="Label13" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_EXH_IN_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_EXH_IN_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label14" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_EXH_IN_2")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_EXH_IN_3_Color").ToString()  %>'>
                                                                <asp:Label ID="Label15" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_EXH_IN_3")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_EXH_OUT_1_Color").ToString()  %>'>
                                                                <asp:Label ID="Label16" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_EXH_OUT_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_EXH_OUT_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label17" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_EXH_OUT_2")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_EXH_OUT_3_Color").ToString()  %>'>
                                                                <asp:Label ID="Label18" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_EXH_OUT_3")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_AIR_IN_1_Color").ToString()  %>'>
                                                                <asp:Label ID="Label19" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_AIR_IN_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_AIR_OUT_1_Color").ToString()  %>'>
                                                                <asp:Label ID="Label20" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_AIR_OUT_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_AIR_in_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label26" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_AIR_IN_2")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_AIR_OUT_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label27" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_AIR_OUT_2")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_AIR_IN_3_Color").ToString()  %>'>
                                                                <asp:Label ID="Label29" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_AIR_IN_3")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_AIR_OUT_3_Color").ToString()  %>'>
                                                                <asp:Label ID="Label31" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_AIR_OUT_3")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_SCAVENGE_Color").ToString()  %>'>
                                                                <asp:Label ID="Label32" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_SCAVENGE")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label33" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_CW_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label35" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_CW_OUT_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label36" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_CW_OUT_2")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label37" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_CW_OUT_3")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_LO_B_1_Color").ToString()  %>'>
                                                                <asp:Label ID="Label38" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_LO_B_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_LO_T_1_Color").ToString()  %>'>
                                                                <asp:Label ID="Label39" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_LO_T_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_LO_B_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label42" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_LO_B_2")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_LO_T_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label43" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_LO_T_2")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_LO_B_3_Color").ToString()  %>'>
                                                                <asp:Label ID="Label44" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_LO_B_3")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"T_LO_T_3_Color").ToString()  %>'>
                                                                <asp:Label ID="Label45" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "T_LO_T_3")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label40" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_SCAVENGE_KGPCMS")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label41" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_EXH_BACK")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"P_PD_AC_1_Color").ToString()  %>'>
                                                                <asp:Label ID="Label46" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_PD_AC_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"P_PD_AC_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label47" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_PD_AC_2")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"P_PD_AC_3_Color").ToString()  %>'>
                                                                <asp:Label ID="Label48" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_PD_AC_3")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label49" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_PD_AF_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label50" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_PD_AF_2")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label51" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_PD_AF_3")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="100%" colspan="3" valign="top">
                                                <asp:Repeater ID="rpEngine2" runat="server">
                                                    <HeaderTemplate>
                                                        <table cellspacing="1" cellpadding="3" rules="all" border="0" style="background-color: White;white-space:nowrap;
                                                            border-color: #efefef; width: 100%;">
                                                            <tr>
                                                                <td colspan="63" align="center" class="HeaderCellColor1">
                                                                    MAIN ENGINES
                                                                </td>
                                                            </tr>
                                                            <tr align="center" class="HeaderCellColor">
                                                                <td rowspan="4">
                                                                    Watch
                                                                </td>
                                                                <td colspan="40">
                                                                    TEMPERATURE C
                                                                </td>
                                                                <td rowspan="4">
                                                                    <label id="Label176" class="verticaltext5">
                                                                        FUEL VISC
                                                                    </label>
                                                                </td>
                                                                <td colspan="12">
                                                                    HEAT EXCHANGERS TEMPERATURES C
                                                                </td>
                                                                <td colspan="9">
                                                                    PRESSURE kg/cm2
                                                                </td>
                                                            </tr>
                                                            <tr align="center" class="HeaderCellColor">
                                                                <td colspan="13">
                                                                    MAIN BEARING
                                                                </td>
                                                                <td colspan="13">
                                                                    JACKET COOLING
                                                                </td>
                                                                <td colspan="13">
                                                                    PISTON COOLING
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label190" class="verticaltext">
                                                                        FUEL OIL</label>
                                                                </td>
                                                                <td colspan="4">
                                                                    JACKET COOLER
                                                                </td>
                                                                <td colspan="4">
                                                                    L O COOLER
                                                                </td>
                                                                <td colspan="4">
                                                                    PISTON COOLER
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label191" class="verticaltext">
                                                                        Sea Water</label>
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label192" class="verticaltext">
                                                                        Jaket Water</label>
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label193" class="verticaltext">
                                                                        Bearing & X-hd LO</label>
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label194" class="verticaltext">
                                                                        Canshaft LO</label>
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label195" class="verticaltext">
                                                                        F V Cooling</label>
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label196" class="verticaltext">
                                                                        Fuel Oil</label>
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label197" class="verticaltext">
                                                                        Piston Cooling</label>
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label198" class="verticaltext">
                                                                        Control Air</label>
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label199" class="verticaltext">
                                                                        Service Air
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                            <tr align="center" class="HeaderCellColor">
                                                                <td rowspan="2">
                                                                    IN
                                                                </td>
                                                                <td colspan="12">
                                                                    OUTLET
                                                                </td>
                                                                <td rowspan="2">
                                                                    IN
                                                                </td>
                                                                <td colspan="12">
                                                                    OUTLET
                                                                </td>
                                                                <td rowspan="2">
                                                                    IN
                                                                </td>
                                                                <td colspan="12">
                                                                    OUTLET
                                                                </td>
                                                                <td colspan="2">
                                                                    SW
                                                                </td>
                                                                <td colspan="2">
                                                                    FW
                                                                </td>
                                                                <td colspan="2">
                                                                    SW
                                                                </td>
                                                                <td colspan="2">
                                                                    LO
                                                                </td>
                                                                <td colspan="2">
                                                                    SW
                                                                </td>
                                                                <td colspan="2">
                                                                    SW/FW
                                                                </td>
                                                            </tr>
                                                            <tr align="center" class="HeaderCellColor">
                                                                <td width="40px">
                                                                    1
                                                                </td>
                                                                <td width="40px">
                                                                    2
                                                                </td>
                                                                <td width="40px">
                                                                    3
                                                                </td>
                                                                <td width="40px">
                                                                    4
                                                                </td>
                                                                <td width="40px">
                                                                    5
                                                                </td>
                                                                <td width="40px">
                                                                    6
                                                                </td>
                                                                <td width="40px">
                                                                    7
                                                                </td>
                                                                <td width="40px">
                                                                    8
                                                                </td>
                                                                <td width="40px">
                                                                    9
                                                                </td>
                                                                <td width="40px">
                                                                    10
                                                                </td>
                                                                <td width="40px">
                                                                    11
                                                                </td>
                                                                <td width="40px">
                                                                    12
                                                                </td>
                                                                <td width="40px">
                                                                    1
                                                                </td>
                                                                <td width="40px">
                                                                    2
                                                                </td>
                                                                <td width="40px">
                                                                    3
                                                                </td>
                                                                <td width="40px">
                                                                    4
                                                                </td>
                                                                <td width="40px">
                                                                    5
                                                                </td>
                                                                <td width="40px">
                                                                    6
                                                                </td>
                                                                <td width="40px">
                                                                    7
                                                                </td>
                                                                <td width="40px">
                                                                    8
                                                                </td>
                                                                <td width="40px">
                                                                    9
                                                                </td>
                                                                <td width="40px">
                                                                    10
                                                                </td>
                                                                <td width="40px">
                                                                    11
                                                                </td>
                                                                <td width="40px">
                                                                    12
                                                                </td>
                                                                <td width="40px">
                                                                    1
                                                                </td>
                                                                <td width="40px">
                                                                    2
                                                                </td>
                                                                <td width="40px">
                                                                    3
                                                                </td>
                                                                <td width="40px">
                                                                    4
                                                                </td>
                                                                <td width="40px">
                                                                    5
                                                                </td>
                                                                <td width="40px">
                                                                    6
                                                                </td>
                                                                <td width="40px">
                                                                    7
                                                                </td>
                                                                <td width="40px">
                                                                    8
                                                                </td>
                                                                <td width="40px">
                                                                    9
                                                                </td>
                                                                <td width="40px">
                                                                    10
                                                                </td>
                                                                <td width="40px">
                                                                    11
                                                                </td>
                                                                <td width="40px">
                                                                    12
                                                                </td>
                                                                <td width="40px">
                                                                    IN
                                                                </td>
                                                                <td width="40px">
                                                                    OUT
                                                                </td>
                                                                <td width="40px">
                                                                    IN
                                                                </td>
                                                                <td width="40px">
                                                                    OUT
                                                                </td>
                                                                <td width="40px">
                                                                    IN
                                                                </td>
                                                                <td width="40px">
                                                                    OUT
                                                                </td>
                                                                <td width="40px">
                                                                    IN
                                                                </td>
                                                                <td width="40px">
                                                                    OUT
                                                                </td>
                                                                <td width="40px">
                                                                    IN
                                                                </td>
                                                                <td width="40px">
                                                                    OUT
                                                                </td>
                                                                <td width="40px">
                                                                    IN
                                                                </td>
                                                                <td width="40px">
                                                                    OUT
                                                                </td>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr align="center">
                                                            <td align="left" style="height: 19px;">
                                                                <asp:Label ID="Label159b" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LOG_WATCH")%>'
                                                                    Width="60"> </asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_IN_Color").ToString()   %>'>
                                                                <asp:Label ID="Label52" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_IN")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_1_Color").ToString()   %>'>
                                                                <asp:Label ID="Label235" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_1")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label236" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_2")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_3_Color").ToString()  %>'>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_3")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_4_Color").ToString()   %>'>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_4")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_5_Color").ToString()   %>'>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_5")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_6_Color").ToString()   %>'>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_6")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_7_Color").ToString()   %>'>
                                                                <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_7")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_8_Color").ToString()   %>'>
                                                                <asp:Label ID="Label53" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_8")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_9_Color").ToString()  %>'>
                                                                <asp:Label ID="Label54" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_9")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_10_Color").ToString()   %>'>
                                                                <asp:Label ID="Label55" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_10")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_11_Color").ToString()  %>'>
                                                                <asp:Label ID="Label56" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_11")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_12_Color").ToString()  %>'>
                                                                <asp:Label ID="Label57" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_12")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_IN_Color").ToString()  %>'>
                                                                <asp:Label ID="Label58" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_IN")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_1_Color").ToString()  %>'>
                                                                <asp:Label ID="Label59" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_1")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label60" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_2")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_3_Color").ToString()  %>'>
                                                                <asp:Label ID="Label61" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_3")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_4_Color").ToString()  %>'>
                                                                <asp:Label ID="Label62" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_4")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_5_Color").ToString()   %>'>
                                                                <asp:Label ID="Label63" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_5")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_6_Color").ToString()   %>'>
                                                                <asp:Label ID="Label64" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_6")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_7_Color").ToString()  %>'>
                                                                <asp:Label ID="Label65" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_7")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_8_Color").ToString()  %>'>
                                                                <asp:Label ID="Label66" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_8")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_9_Color").ToString()  %>'>
                                                                <asp:Label ID="Label67" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_9")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_10_Color").ToString()   %>'>
                                                                <asp:Label ID="Label68" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_10")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_11_Color").ToString()  %>'>
                                                                <asp:Label ID="Label69" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_11")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_12_Color").ToString()   %>'>
                                                                <asp:Label ID="Label70" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_12")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_IN_Color").ToString()   %>'>
                                                                <asp:Label ID="Label71" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_IN")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_1_Color").ToString()  %>'>
                                                                <asp:Label ID="Label72" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_1")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label73" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_2")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_3_Color").ToString()  %>'>
                                                                <asp:Label ID="Label74" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_3")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_4_Color").ToString()   %>'>
                                                                <asp:Label ID="Label75" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_4")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_5_Color").ToString()  %>'>
                                                                <asp:Label ID="Label76" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_5")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_6_Color").ToString()  %>'>
                                                                <asp:Label ID="Label77" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_6")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_7_Color").ToString()  %>'>
                                                                <asp:Label ID="Label78" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_7")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_8_Color").ToString()  %>'>
                                                                <asp:Label ID="Label79" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_8")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_9_Color").ToString()  %>'>
                                                                <asp:Label ID="Label80" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_9")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_10_Color").ToString()   %>'>
                                                                <asp:Label ID="Label81" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_10")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_11_Color").ToString()   %>'>
                                                                <asp:Label ID="Label82" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_11")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_12_Color").ToString()  %>'>
                                                                <asp:Label ID="Label83" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_12")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"FUEL_OIL_Color").ToString()  %>'>
                                                                <asp:Label ID="Label160" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FUEL_OIL")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label161" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FUEL_VISC")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_SW_IN")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label21" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_SW_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_FW_IN_Color").ToString()   %>'>
                                                                <asp:Label ID="Label22" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_FW_IN")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_FW_OUT_Color").ToString()  %>'>
                                                                <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_FW_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label168" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LC_SW_IN")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label23" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LC_SW_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"LC_LO_IN_Color").ToString()  %>'>
                                                                <asp:Label ID="Label24" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LC_LO_IN")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"LC_LO_OUT_Color").ToString()  %>'>
                                                                <asp:Label ID="Label25" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LC_LO_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label28" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_SW_IN")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label30" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_SW_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_LO_IN_Color").ToString()  %>'>
                                                                <asp:Label ID="Label34" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_LO_IN")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_LO_OUT_Color").ToString()  %>'>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_LO_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_SEA_WATER")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"P_JACKET_WATER_Color").ToString()  %>'>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_JACKET_WATER")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"P_BEARING_XND_LO_Color").ToString()  %>'>
                                                                <asp:Label ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_BEARING_XND_LO")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"P_CAMSHAFT_LO_Color").ToString()   %>'>
                                                                <asp:Label ID="Label12" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_CAMSHAFT_LO")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"P_FV_COOLING_Color").ToString()  %>'>
                                                                <asp:Label ID="Label13" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_FV_COOLING")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"P_FUEL_OIL_Color").ToString()  %>'>
                                                                <asp:Label ID="Label14" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_FUEL_OIL")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"P_PISTON_COOLING_Color").ToString()  %>'>
                                                                <asp:Label ID="Label15" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_PISTON_COOLING")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"P_CONTROL_AIR_Color").ToString()  %>'>
                                                                <asp:Label ID="Label16" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_CONTROL_AIR")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label17" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_SERVICE_AIR")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr style="background-color: #efefcf; border: 1px solid ActiveBorder" align="center">
                                                            <td align="left" style="height: 19px;">
                                                                <asp:Label ID="Label159c" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LOG_WATCH")%>'> </asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_IN_Color").ToString()   %>'>
                                                                <asp:Label ID="Label52" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_IN")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_1_Color").ToString()   %>'>
                                                                <asp:Label ID="Label235" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_1")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label236" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_2")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_3_Color").ToString()  %>'>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_3")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_4_Color").ToString()   %>'>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_4")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_5_Color").ToString()   %>'>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_5")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_6_Color").ToString()   %>'>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_6")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_7_Color").ToString()   %>'>
                                                                <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_7")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_8_Color").ToString()   %>'>
                                                                <asp:Label ID="Label53" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_8")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_9_Color").ToString()  %>'>
                                                                <asp:Label ID="Label54" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_9")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_10_Color").ToString()   %>'>
                                                                <asp:Label ID="Label55" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_10")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_11_Color").ToString()  %>'>
                                                                <asp:Label ID="Label56" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_11")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MB_OUT_12_Color").ToString()  %>'>
                                                                <asp:Label ID="Label57" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_12")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_IN_Color").ToString()  %>'>
                                                                <asp:Label ID="Label58" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_IN")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_1_Color").ToString()  %>'>
                                                                <asp:Label ID="Label59" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_1")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label60" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_2")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_3_Color").ToString()  %>'>
                                                                <asp:Label ID="Label61" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_3")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_4_Color").ToString()  %>'>
                                                                <asp:Label ID="Label62" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_4")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_5_Color").ToString()   %>'>
                                                                <asp:Label ID="Label63" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_5")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_6_Color").ToString()   %>'>
                                                                <asp:Label ID="Label64" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_6")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_7_Color").ToString()  %>'>
                                                                <asp:Label ID="Label65" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_7")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_8_Color").ToString()  %>'>
                                                                <asp:Label ID="Label66" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_8")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_9_Color").ToString()  %>'>
                                                                <asp:Label ID="Label67" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_9")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_10_Color").ToString()   %>'>
                                                                <asp:Label ID="Label68" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_10")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_11_Color").ToString()  %>'>
                                                                <asp:Label ID="Label69" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_11")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_OUT_12_Color").ToString()   %>'>
                                                                <asp:Label ID="Label70" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_12")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_IN_Color").ToString()   %>'>
                                                                <asp:Label ID="Label71" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_IN")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_1_Color").ToString()  %>'>
                                                                <asp:Label ID="Label72" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_1")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label73" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_2")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_3_Color").ToString()  %>'>
                                                                <asp:Label ID="Label74" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_3")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_4_Color").ToString()   %>'>
                                                                <asp:Label ID="Label75" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_4")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_5_Color").ToString()  %>'>
                                                                <asp:Label ID="Label76" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_5")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_6_Color").ToString()  %>'>
                                                                <asp:Label ID="Label77" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_6")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_7_Color").ToString()  %>'>
                                                                <asp:Label ID="Label78" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_7")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_8_Color").ToString()  %>'>
                                                                <asp:Label ID="Label79" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_8")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_9_Color").ToString()  %>'>
                                                                <asp:Label ID="Label80" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_9")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_10_Color").ToString()   %>'>
                                                                <asp:Label ID="Label81" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_10")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_11_Color").ToString()   %>'>
                                                                <asp:Label ID="Label82" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_11")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_OUT_12_Color").ToString()  %>'>
                                                                <asp:Label ID="Label83" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_12")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"FUEL_OIL_Color").ToString()  %>'>
                                                                <asp:Label ID="Label160" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FUEL_OIL")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label161" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FUEL_VISC")%>'></asp:Label>
                                                            </td>
                                                            <td class='CellClass0'>
                                                                <asp:Label ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_SW_IN")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label21" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_SW_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_FW_IN_Color").ToString()   %>'>
                                                                <asp:Label ID="Label22" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_FW_IN")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"JC_FW_OUT_Color").ToString()  %>'>
                                                                <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JC_FW_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label168" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LC_SW_IN")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label23" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LC_SW_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"LC_LO_IN_Color").ToString()  %>'>
                                                                <asp:Label ID="Label24" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LC_LO_IN")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"LC_LO_OUT_Color").ToString()  %>'>
                                                                <asp:Label ID="Label25" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LC_LO_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label28" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_SW_IN")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label30" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_SW_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_LO_IN_Color").ToString()  %>'>
                                                                <asp:Label ID="Label34" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_LO_IN")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PC_LO_OUT_Color").ToString()  %>'>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PC_LO_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_SEA_WATER")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"P_JACKET_WATER_Color").ToString()  %>'>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_JACKET_WATER")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"P_BEARING_XND_LO_Color").ToString()  %>'>
                                                                <asp:Label ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_BEARING_XND_LO")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"P_CAMSHAFT_LO_Color").ToString()   %>'>
                                                                <asp:Label ID="Label12" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_CAMSHAFT_LO")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"P_FV_COOLING_Color").ToString()  %>'>
                                                                <asp:Label ID="Label13" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_FV_COOLING")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"P_FUEL_OIL_Color").ToString()  %>'>
                                                                <asp:Label ID="Label14" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_FUEL_OIL")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"P_PISTON_COOLING_Color").ToString()  %>'>
                                                                <asp:Label ID="Label15" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_PISTON_COOLING")%>'></asp:Label>
                                                            </td>
                                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"P_CONTROL_AIR_Color").ToString()  %>'>
                                                                <asp:Label ID="Label16" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_CONTROL_AIR")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label17" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "P_SERVICE_AIR")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="100%" valign="top" colspan="2">
                                                <asp:Repeater ID="rpEngine3" runat="server">
                                                    <HeaderTemplate>
                                                        <table cellspacing="1" cellpadding="3" rules="all" border="0" style="background-color: White;white-space:nowrap;
                                                            border-color: #efefef; width: 100%;">
                                                            <tr align="center" class="HeaderCellColor1">
                                                                <td>
                                                                </td>
                                                                <td colspan="12">
                                                                    AIR CONDITIONING
                                                                </td>
                                                                <td colspan="7">
                                                                    REFRIGERATION PLANT
                                                                </td>
                                                                <td colspan="9">
                                                                    FRESH WATER GENERATOR
                                                                </td>
                                                                <td colspan="4">
                                                                    BOILER
                                                                </td>
                                                                <td colspan="5">
                                                                    PURIFIERS
                                                                </td>
                                                                <td colspan="11">
                                                                    MISCELLANEOUS
                                                                </td>
                                                            </tr>
                                                            <tr align="center" class="HeaderCellColor">
                                                                <td rowspan="3">
                                                                    Watch
                                                                </td>
                                                                <td colspan="8">
                                                                    Pressure-kg/cm2
                                                                </td>
                                                                <td colspan="4">
                                                                    Air Temp C
                                                                </td>
                                                                <td colspan="4">
                                                                    Pressure-kg/cm2
                                                                </td>
                                                                <td colspan="3">
                                                                    Temp C
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label192" class="verticaltext">
                                                                        Running Hours
                                                                    </label>
                                                                </td>
                                                                <td colspan="4">
                                                                    Heat Exchanger C
                                                                </td>
                                                                <td rowspan="3" c>
                                                                    <label id="Label200" class="verticaltext">
                                                                        Vacuum cm. Hg
                                                                    </label>
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label201" class="verticaltext">
                                                                        Shell Temp C
                                                                    </label>
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label202" class="verticaltext">
                                                                        Salinity PPM
                                                                    </label>
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label203" class="verticaltext">
                                                                        Flowmeter
                                                                    </label>
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label204" class="verticaltext">
                                                                        Oile Finning Hours
                                                                    </label>
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label205" class="verticaltext">
                                                                        Stream Press.</label>
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label206" class="verticaltext">
                                                                        Feed Water temp.
                                                                    </label>
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label207" class="verticaltext">
                                                                        EGE soot Bown
                                                                    </label>
                                                                </td>
                                                                <td colspan="2">
                                                                    HO
                                                                </td>
                                                                <td colspan="2">
                                                                    LO
                                                                </td>
                                                                <td>
                                                                    DO
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label208" class="verticaltext">
                                                                        Staff grounding
                                                                    </label>
                                                                </td>
                                                                <td colspan="9">
                                                                    Temperature C
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label209" class="verticaltext">
                                                                        incinerator Run Hrs.
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                            <tr align="center" class="HeaderCellColor">
                                                                <td colspan="4">
                                                                    P
                                                                </td>
                                                                <td colspan="4">
                                                                    S
                                                                </td>
                                                                <td colspan="2">
                                                                    P
                                                                </td>
                                                                <td colspan="2">
                                                                    S
                                                                </td>
                                                                <td rowspan="2">
                                                                    <label id="Label210" class="verticaltext1">
                                                                        Comp. No.
                                                                    </label>
                                                                </td>
                                                                <td rowspan="2">
                                                                    <label id="Label211" class="verticaltext1">
                                                                        Suct
                                                                    </label>
                                                                </td>
                                                                <td rowspan="2">
                                                                    <label id="Label212" class="verticaltext1">
                                                                        Disch
                                                                    </label>
                                                                </td>
                                                                <td rowspan="2">
                                                                    <label id="Label213" class="verticaltext1">
                                                                        L. O.
                                                                    </label>
                                                                </td>
                                                                <td rowspan="2">
                                                                    <label id="Label214" class="verticaltext1">
                                                                        Meat
                                                                    </label>
                                                                </td>
                                                                <td rowspan="2">
                                                                    <label id="Label215" class="verticaltext1">
                                                                        Fish
                                                                    </label>
                                                                </td>
                                                                <td rowspan="2">
                                                                    <label id="Label216" class="verticaltext1">
                                                                        Veg/Labby
                                                                    </label>
                                                                </td>
                                                                <td colspan="2">
                                                                    FW
                                                                </td>
                                                                <td colspan="2">
                                                                    SW
                                                                </td>
                                                                <td rowspan="2">
                                                                    <label id="Label217" class="verticaltext1">
                                                                        Hrs. run
                                                                    </label>
                                                                </td>
                                                                <td rowspan="2">
                                                                    <label id="Label218" class="verticaltext1">
                                                                        Temp
                                                                    </label>
                                                                </td>
                                                                <td rowspan="2">
                                                                    <label id="Label219" class="verticaltext1">
                                                                        Hrs. run
                                                                    </label>
                                                                </td>
                                                                <td rowspan="2">
                                                                    <label id="Label220" class="verticaltext1">
                                                                        Temp
                                                                    </label>
                                                                </td>
                                                                <td rowspan="2">
                                                                    <label id="Label221" class="verticaltext1">
                                                                        Temp
                                                                    </label>
                                                                </td>
                                                                <td rowspan="2">
                                                                    <label id="Label222" class="verticaltext1">
                                                                        Thust bng.
                                                                    </label>
                                                                </td>
                                                                <td rowspan="2">
                                                                    <label id="Label223" class="verticaltext1">
                                                                        intam big
                                                                    </label>
                                                                </td>
                                                                <td rowspan="2">
                                                                    <label id="Label224" class="verticaltext1">
                                                                        Stem tube Oil
                                                                    </label>
                                                                </td>
                                                                <td rowspan="2">
                                                                    <label id="Label225" class="verticaltext1">
                                                                        Sea water
                                                                    </label>
                                                                </td>
                                                                <td rowspan="2">
                                                                    <label id="Label226" class="verticaltext1">
                                                                        E R
                                                                    </label>
                                                                </td>
                                                                <td colspan="2" width="40px">
                                                                    H.O. Sett.
                                                                </td>
                                                                <td colspan="2" width="40px">
                                                                    H.O. Serv.
                                                                </td>
                                                            </tr>
                                                            <tr align="center" class="HeaderCellColor">
                                                                <td>
                                                                    <label id="Label227" class="verticaltext2">
                                                                        Suct
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label id="Label228" class="verticaltext2">
                                                                        Disch
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label id="Label229" class="verticaltext2">
                                                                        L. O.
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label id="Label230" class="verticaltext2">
                                                                        Cooling Water
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label id="Label231" class="verticaltext2">
                                                                        Suct
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label id="Label233" class="verticaltext2">
                                                                        Disch
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label id="Label237" class="verticaltext2">
                                                                        L. O.
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label id="Label238" class="verticaltext2">
                                                                        Cooling Water
                                                                    </label>
                                                                </td>
                                                                <td width="50px">
                                                                    IN
                                                                </td>
                                                                <td width="50px">
                                                                    OUT
                                                                </td>
                                                                <td width="40px">
                                                                    IN
                                                                </td>
                                                                <td width="40px">
                                                                    OUT
                                                                </td>
                                                                <td width="40px">
                                                                    IN
                                                                </td>
                                                                <td width="40px">
                                                                    OUT
                                                                </td>
                                                                <td width="40px">
                                                                    IN
                                                                </td>
                                                                <td width="40px">
                                                                    OUT
                                                                </td>
                                                                <td width="40px">
                                                                    1
                                                                </td>
                                                                <td width="40px">
                                                                    2
                                                                </td>
                                                                <td width="40px">
                                                                    1
                                                                </td>
                                                                <td width="40px">
                                                                    2
                                                                </td>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr align="center">
                                                            <td align="left" style="height: 19px;">
                                                                <asp:Label ID="Label159d" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LOG_WATCH")%>'
                                                                    Width="60"> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label52" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_SUCT_PRESS")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label235" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_DISCH_PRESS")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label236" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_LO_PRESS")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_CW_PRESS")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_SUCT_PRESS")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_DISCH_PRESS")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_LO_PRESS")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='CellClass0'>
                                                                <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_CW_PRESS")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label53" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_IN_AIR_TEMP")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label54" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_OUT_AIR_TEMP")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label55" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_IN_AIR_TEMP")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label56" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_OUT_AIR_TEMP")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label57" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "REF_COMP_NO")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label58" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "REF_SUCT_PRESS")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label59" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "REF_DISCH_PRESS")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label60" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "REF_LO_PRESS")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"REF_MEAT_TEMP_Color").ToString()  %>'>
                                                                <asp:Label ID="Label61" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "REF_MEAT_TEMP")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"REF_FISH_TEMP_Color").ToString()  %>'>
                                                                <asp:Label ID="Label62" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "REF_FISH_TEMP")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"REF_VEG_LOBBY_TEMP_Color").ToString()  %>'>
                                                                <asp:Label ID="Label63" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "REF_VEG_LOBBY_TEMP")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label64" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_RH")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label65" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_FW_IN")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label66" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_FW_OUT")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label67" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_SW_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label68" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_SW_OUT")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"FWGEN_VACCUM_Color").ToString()  %>'>
                                                                <asp:Label ID="Label69" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_VACCUM")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"FWGEN_SHELL_TEMP_Color").ToString()  %>'>
                                                                <asp:Label ID="Label70" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_SHELL_TEMP")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"FWGEN_SALINITY_PPM_Color").ToString()  %>'>
                                                                <asp:Label ID="Label71" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_SALINITY_PPM")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label72" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_FLOWMETER")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label73" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BLR_OIL_FIRING_HRS")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label74" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BLR_STEAM_PRESS")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label75" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BLR_FEED_WTR_TEMP")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label76" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BLR_EGE_SOOT_BLOWN")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label77" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_HO_RH")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PUR_HO_TEMP_Color").ToString()  %>'>
                                                                <asp:Label ID="Label78" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_HO_TEMP")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label79" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_LO_RH")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PUR_LO_TEMP_Color").ToString()   %>'>
                                                                <asp:Label ID="Label80" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_LO_TEMP")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label81" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_DO_TEMP")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label82" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_SFT_GROUNDING")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MISC_THRUST_BRG_TEMP_Color").ToString()  %>'>
                                                                <asp:Label ID="Label83" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_THRUST_BRG_TEMP")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MISC_INTERM_BRG_TEMP_Color").ToString()  %>'>
                                                                <asp:Label ID="Label160" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_INTERM_BRG_TEMP")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MISC_STERN_TUBE_OIL_TEMP_Color").ToString()  %>'>
                                                                <asp:Label ID="Label161" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_STERN_TUBE_OIL_TEMP")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_SEA_WTR_TEMP")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label21" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_ER_TEMP")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MISC_HO_SETT_1_Color").ToString()   %>'>
                                                                <asp:Label ID="Label22" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SETT_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MISC_HO_SETT_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SETT_2")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MISC_HO_SERV_1_Color").ToString()  %>'>
                                                                <asp:Label ID="Label168" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SERV_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MISC_HO_SERV_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label23" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SERV_2")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='CellClass0'>
                                                                <asp:Label ID="Label24" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_INCINERATOR_RH")%>'> </asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr style="background-color: #efefcf; border: 1px solid ActiveBorder" align="center">
                                                            <td align="left" style="height: 19px;">
                                                                <asp:Label ID="Label159e" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LOG_WATCH")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label52" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_SUCT_PRESS")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label235" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_DISCH_PRESS")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label236" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_LO_PRESS")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_CW_PRESS")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_SUCT_PRESS")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_DISCH_PRESS")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_LO_PRESS")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_CW_PRESS")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label53" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_IN_AIR_TEMP")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label54" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_OUT_AIR_TEMP")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='CellClass0'>
                                                                <asp:Label ID="Label55" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_IN_AIR_TEMP")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label56" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_OUT_AIR_TEMP")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label57" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "REF_COMP_NO")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label58" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "REF_SUCT_PRESS")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label59" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "REF_DISCH_PRESS")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label60" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "REF_LO_PRESS")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"REF_MEAT_TEMP_Color").ToString()  %>'>
                                                                <asp:Label ID="Label61" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "REF_MEAT_TEMP")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"REF_FISH_TEMP_Color").ToString()  %>'>
                                                                <asp:Label ID="Label62" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "REF_FISH_TEMP")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"REF_VEG_LOBBY_TEMP_Color").ToString()  %>'>
                                                                <asp:Label ID="Label63" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "REF_VEG_LOBBY_TEMP")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label64" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_RH")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label65" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_FW_IN")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label66" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_FW_OUT")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label67" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_SW_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label68" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_SW_OUT")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"FWGEN_VACCUM_Color").ToString()  %>'>
                                                                <asp:Label ID="Label69" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_VACCUM")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"FWGEN_SHELL_TEMP_Color").ToString()  %>'>
                                                                <asp:Label ID="Label70" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_SHELL_TEMP")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"FWGEN_SALINITY_PPM_Color").ToString()  %>'>
                                                                <asp:Label ID="Label71" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_SALINITY_PPM")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label72" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_FLOWMETER")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label73" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BLR_OIL_FIRING_HRS")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label74" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BLR_STEAM_PRESS")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label75" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BLR_FEED_WTR_TEMP")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label76" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BLR_EGE_SOOT_BLOWN")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='CellClass0'>
                                                                <asp:Label ID="Label77" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_HO_RH")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PUR_HO_TEMP_Color").ToString()  %>'>
                                                                <asp:Label ID="Label78" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_HO_TEMP")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label79" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_LO_RH")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"PUR_LO_TEMP_Color").ToString()   %>'>
                                                                <asp:Label ID="Label80" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_LO_TEMP")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label81" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_DO_TEMP")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label82" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_SFT_GROUNDING")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MISC_THRUST_BRG_TEMP_Color").ToString()  %>'>
                                                                <asp:Label ID="Label83" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_THRUST_BRG_TEMP")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MISC_INTERM_BRG_TEMP_Color").ToString()  %>'>
                                                                <asp:Label ID="Label160" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_INTERM_BRG_TEMP")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MISC_STERN_TUBE_OIL_TEMP_Color").ToString()  %>'>
                                                                <asp:Label ID="Label161" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_STERN_TUBE_OIL_TEMP")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_SEA_WTR_TEMP")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label21" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_ER_TEMP")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MISC_HO_SETT_1_Color").ToString()   %>'>
                                                                <asp:Label ID="Label22" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SETT_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MISC_HO_SETT_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SETT_2")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MISC_HO_SERV_1_Color").ToString()  %>'>
                                                                <asp:Label ID="Label168" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SERV_1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"MISC_HO_SERV_2_Color").ToString()  %>'>
                                                                <asp:Label ID="Label23" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SERV_2")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 19px;" class="CellClass0">
                                                                <asp:Label ID="Label24" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_INCINERATOR_RH")%>'> </asp:Label>
                                                            </td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </td>
                                            <td valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="50%" rowspan="2" valign="top">
                                                <asp:Repeater ID="rpTrainingDetails" runat="server">
                                                    <HeaderTemplate>
                                                        <table cellspacing="1" cellpadding="3" rules="all" border="0" style="background-color: White;white-space:nowrap;
                                                            border-color: #efefef; width: 100%;">
                                                            <tr class="HeaderCellColor1">
                                                                <td colspan="22" align="center">
                                                                    GENERATOR ENGINES
                                                                </td>
                                                            </tr>
                                                            <tr align="center" class="HeaderCellColor">
                                                                <td rowspan="4">
                                                                    Watch
                                                                </td>
                                                                <td rowspan="4">
                                                                    <label id="Label192" class="verticaltext1">
                                                                        Generator no.</label>
                                                                </td>
                                                                <td rowspan="4">
                                                                    <label id="Label150" class="verticaltext1">
                                                                        Running hrs.</label>
                                                                </td>
                                                                <td colspan="13">
                                                                    Temperature C
                                                                </td>
                                                                <td colspan="4">
                                                                    Pressure-kg/cm2
                                                                </td>
                                                                <td rowspan="4">
                                                                    <label id="Label246" class="verticaltext1">
                                                                        Amps
                                                                    </label>
                                                                </td>
                                                                <td rowspan="4">
                                                                    <label id="Label247" class="verticaltext1">
                                                                        kW
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                            <tr align="center" class="HeaderCellColor">
                                                                <td colspan="2" rowspan="2">
                                                                    Exhaust
                                                                </td>
                                                                <td colspan="2" rowspan="2">
                                                                    CW
                                                                </td>
                                                                <td colspan="2" rowspan="2">
                                                                    LO
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label239" class="verticaltext2">
                                                                        Boost Air
                                                                    </label>
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label240" class="verticaltext2">
                                                                        Pedestal Bearing
                                                                    </label>
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label241" class="verticaltext2">
                                                                        Fuel IN
                                                                    </label>
                                                                </td>
                                                                <td colspan="4">
                                                                    A/E LO COOLER
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label242" class="verticaltext2">
                                                                        L. O.</label>
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label243" class="verticaltext2">
                                                                        C. W.
                                                                    </label>
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label244" class="verticaltext2">
                                                                        Boost Air
                                                                    </label>
                                                                </td>
                                                                <td rowspan="3">
                                                                    <label id="Label245" class="verticaltext2">
                                                                        Fuel Oil
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                            <tr align="center" class="HeaderCellColor">
                                                                <td colspan="2">
                                                                    SW
                                                                </td>
                                                                <td colspan="2">
                                                                    LO
                                                                </td>
                                                            </tr>
                                                            <tr align="center" class="HeaderCellColor">
                                                                <td>
                                                                    Max
                                                                </td>
                                                                <td>
                                                                    Min
                                                                </td>
                                                                <td>
                                                                    In
                                                                </td>
                                                                <td>
                                                                    Out
                                                                </td>
                                                                <td>
                                                                    In
                                                                </td>
                                                                <td>
                                                                    Out
                                                                </td>
                                                                <td>
                                                                    In
                                                                </td>
                                                                <td>
                                                                    Out
                                                                </td>
                                                                <td>
                                                                    In
                                                                </td>
                                                                <td>
                                                                    Out
                                                                </td>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr align="center">
                                                            <td align="left" rowspan="4" style="height: 18px;width:125px">
                                                                <asp:Label ID="Label159f" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LOG_WATCH")%>'  > </asp:Label>
                                                            </td>
                                                            <td style="height: 18px;">
                                                                <asp:Label ID="Label234" runat="server" Text='1'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label235" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE1_RUNNING_HR")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE1_TEMP_EXH_MAX_Color").ToString()  %>'>
                                                                <asp:Label ID="Label236" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE1_TEMP_EXH_MAX")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE1_TEMP_EXH_MIN_Color").ToString()  %>'>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE1_TEMP_EXH_MIN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE1_TEMP_CW_IN_Color").ToString()  %>'>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE1_TEMP_CW_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE1_TEMP_CW_OUT_Color").ToString()  %>'>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE1_TEMP_CW_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE1_TEMP_LO_IN_Color").ToString()  %>'>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE1_TEMP_LO_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE1_TEMP_LO_OUT_Color").ToString()  %>'>
                                                                <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE1_TEMP_LO_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label160" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE1_TEMP_BOOSTAIR")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label161" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE1_TEMP_PDL_BEARING")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE1_TEMP_FUEL_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label21" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE1_TEMP_AE_SW_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label22" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE1_TEMP_AE_SW_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE1_TEMP_AE_LO_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label168" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE1_TEMP_AE_LO_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE1_PRESS_LO_Color").ToString()  %>'>
                                                                <asp:Label ID="Label23" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE1_PRESS_LO")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE1_PRESS_CW_Color").ToString()   %>'>
                                                                <asp:Label ID="Label24" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE1_PRESS_CW")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label25" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE1_PRESS_BOOST_AIR")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label28" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE1_PRESS_FUEL_OIL")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label30" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE1_AMPS")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label34" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE1_KW")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="background-color: #efefcf; border: 1px solid ActiveBorder" align="center">
                                                            <td style="height: 18px;">
                                                                <asp:Label ID="Label7" runat="server" Text='2'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label162" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE2_RUNNING_HR")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE2_TEMP_EXH_MAX_Color").ToString()   %>'>
                                                                <asp:Label ID="Label163" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE2_TEMP_EXH_MAX")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE2_TEMP_EXH_MIN_Color").ToString()  %>'>
                                                                <asp:Label ID="Label169" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE2_TEMP_EXH_MIN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE2_TEMP_CW_IN_Color").ToString()  %>'>
                                                                <asp:Label ID="Label170" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE2_TEMP_CW_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE2_TEMP_CW_OUT_Color").ToString()  %>'>
                                                                <asp:Label ID="Label272" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE2_TEMP_CW_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE2_TEMP_LO_IN_Color").ToString()  %>'>
                                                                <asp:Label ID="Label273" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE2_TEMP_LO_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE2_TEMP_LO_OUT_Color").ToString()  %>'>
                                                                <asp:Label ID="Label274" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE2_TEMP_LO_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label275" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE2_TEMP_BOOSTAIR")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label280" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE2_TEMP_PDL_BEARING")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label281" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE2_TEMP_FUEL_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label282" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE2_TEMP_AE_SW_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label283" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE2_TEMP_AE_SW_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label284" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE2_TEMP_AE_LO_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label285" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE2_TEMP_AE_LO_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE2_PRESS_LO_Color").ToString()  %>'>
                                                                <asp:Label ID="Label286" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE2_PRESS_LO")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE2_PRESS_CW_Color").ToString()  %>'>
                                                                <asp:Label ID="Label287" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE2_PRESS_CW")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label288" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE2_PRESS_BOOST_AIR")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label289" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE2_PRESS_FUEL_OIL")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label290" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE2_AMPS")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label291" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE2_KW")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td style="height: 18px;">
                                                                <asp:Label ID="Label123" runat="server" Text='3'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label134" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE3_RUNNING_HR")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE3_TEMP_EXH_MAX_Color").ToString()  %>'>
                                                                <asp:Label ID="Label135" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE3_TEMP_EXH_MAX")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE3_TEMP_EXH_MIN_Color").ToString()  %>'>
                                                                <asp:Label ID="Label136" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE3_TEMP_EXH_MIN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE3_TEMP_CW_IN_Color").ToString()  %>'>
                                                                <asp:Label ID="Label137" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE3_TEMP_CW_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE3_TEMP_CW_OUT_Color").ToString()  %>'>
                                                                <asp:Label ID="Label138" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE3_TEMP_CW_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE3_TEMP_LO_IN_Color").ToString()  %>'>
                                                                <asp:Label ID="Label139" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE3_TEMP_LO_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE3_TEMP_LO_OUT_Color").ToString()  %>'>
                                                                <asp:Label ID="Label140" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE3_TEMP_LO_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label164" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE3_TEMP_BOOSTAIR")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label165" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE3_TEMP_PDL_BEARING")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label171" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE3_TEMP_FUEL_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label292" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE3_TEMP_AE_SW_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label293" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE3_TEMP_AE_SW_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label294" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE3_TEMP_AE_LO_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label295" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE3_TEMP_AE_LO_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE3_PRESS_LO_Color").ToString()  %>'>
                                                                <asp:Label ID="Label296" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE3_PRESS_LO")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE3_PRESS_CW_Color").ToString()   %>'>
                                                                <asp:Label ID="Label297" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE3_PRESS_CW")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label298" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE3_PRESS_BOOST_AIR")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label299" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE3_PRESS_FUEL_OIL")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label300" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE3_AMPS")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label301" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE3_KW")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="background-color: #efefcf; border: 1px solid ActiveBorder" align="center">
                                                            <td style="height: 18px;">
                                                                <asp:Label ID="Label141" runat="server" Text='4'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label142" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE4_RUNNING_HR")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE4_TEMP_EXH_MAX_Color").ToString()  %>'>
                                                                <asp:Label ID="Label143" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE4_TEMP_EXH_MAX")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE4_TEMP_EXH_MIN_Color").ToString()  %>'>
                                                                <asp:Label ID="Label144" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE4_TEMP_EXH_MIN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE4_TEMP_CW_IN_Color").ToString()  %>'>
                                                                <asp:Label ID="Label145" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE4_TEMP_CW_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE4_TEMP_CW_OUT_Color").ToString()  %>'>
                                                                <asp:Label ID="Label146" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE4_TEMP_CW_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE4_TEMP_LO_IN_Color").ToString()  %>'>
                                                                <asp:Label ID="Label147" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE4_TEMP_LO_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE4_TEMP_LO_OUT_Color").ToString()  %>'>
                                                                <asp:Label ID="Label148" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE4_TEMP_LO_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label149" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE4_TEMP_BOOSTAIR")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label151" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE4_TEMP_PDL_BEARING")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label152" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE4_TEMP_FUEL_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label153" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE4_TEMP_AE_SW_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label154" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE4_TEMP_AE_SW_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label155" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE4_TEMP_AE_LO_IN")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label156" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE4_TEMP_AE_LO_OUT")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE4_PRESS_LO_Color").ToString()  %>'>
                                                                <asp:Label ID="Label157" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE4_PRESS_LO")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"GE4_PRESS_CW_Color").ToString()  %>'>
                                                                <asp:Label ID="Label158" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE4_PRESS_CW")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label166" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE4_PRESS_BOOST_AIR")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label167" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE4_PRESS_FUEL_OIL")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label172" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE4_AMPS")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label302" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GE4_KW")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                                <asp:Repeater ID="Repeater2" runat="server">
                                                    <HeaderTemplate>
                                                        <table cellspacing="0" cellpadding="3" rules="all" border="0" style="background-color: White;white-space:nowrap;
                                                            border-color: #efefef; width: 100%;">
                                                            <tr class="HeaderCellColor1">
                                                                <td>
                                                                </td>
                                                                <td colspan="11" align="center">
                                                                    TURBO ALTERNATOR
                                                                </td>
                                                                <td colspan="9" align="center">
                                                                    SHAFT GENERATOR
                                                                </td>
                                                            </tr>
                                                            <tr align="center" class="HeaderCellColor">
                                                                <td width="90px">
                                                                    Watch
                                                                </td>
                                                                <td>
                                                                    Run Hrs.
                                                                </td>
                                                                <td>
                                                                    <label id="Label239" class="verticaltext2">
                                                                        Steam Press.
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label id="Label248" class="verticaltext2">
                                                                        Cond Vac.
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label id="Label249" class="verticaltext2">
                                                                        Gland Steam
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label id="Label250" class="verticaltext2">
                                                                        LO Press
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label id="Label251" class="verticaltext2">
                                                                        L. O. Temp
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label id="Label252" class="verticaltext2">
                                                                        Thrust brg C
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label id="Label253" class="verticaltext2">
                                                                        Free end C
                                                                    </label>
                                                                </td>
                                                                <td style="width: 40px">
                                                                </td>
                                                                <td style="width: 40px">
                                                                    KW
                                                                </td>
                                                                <td style="width: 40px">
                                                                    Amps
                                                                </td>
                                                                <td>
                                                                    Run Hrs.
                                                                </td>
                                                                <td>
                                                                    <label id="Label254" class="verticaltext2">
                                                                        Clutch Air Pr.
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label id="Label255" class="verticaltext2">
                                                                        LO Press
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label id="Label256" class="verticaltext2">
                                                                        LO Temp.</label>
                                                                </td>
                                                                <td style="width: 40px">
                                                                </td>
                                                                <td colspan="2">
                                                                    SG Cont Brg Temp
                                                                </td>
                                                                <td style="width: 40px">
                                                                    KW
                                                                </td>
                                                                <td style="width: 40px">
                                                                    Amps
                                                                </td>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr align="center">
                                                            <td align="left" style="height: 18px;">
                                                                <asp:Label ID="Label159k" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LOG_WATCH")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label235" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TA_RUNNING_HR")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label236" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TA_STEAM_PRESS")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TA_COND_VAC")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TA_GLAND_STEAM")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TA_LO_PRESS")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TA_LO_TEMP")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TA_THUST_BIG")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label160" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TA_FREE_END")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;">
                                                                <asp:Label ID="Label161" runat="server" Text=' '> </asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TA_KW")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label21" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TA_AMPS")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label22" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SG_RUNNING_HRG")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SG_CLUTCH_AIR_PR")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"SG_LO_PRESS_Color").ToString()  %>'>
                                                                <asp:Label ID="Label168" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SG_LO_PRESS")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"SG_LO_TEMP_Color").ToString()  %>'>
                                                                <asp:Label ID="Label23" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SG_LO_TEMP")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;">
                                                                <asp:Label ID="Label234" runat="server" Text=' '> </asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label24" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SG_SG_COND_BRG_TEMP1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label25" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SG_SG_COND_BRG_TEMP2")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label30" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SG_KW")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label34" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SG_AMPS")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr style="background-color: #efefcf; border: 1px solid ActiveBorder" align="center">
                                                            <td align="left" style="height: 18px;">
                                                                <asp:Label ID="Label159l" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LOG_WATCH")%>'> </asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label235" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TA_RUNNING_HR")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label236" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TA_STEAM_PRESS")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TA_COND_VAC")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TA_GLAND_STEAM")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TA_LO_PRESS")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TA_LO_TEMP")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TA_THUST_BIG")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label160" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TA_FREE_END")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;">
                                                                <asp:Label ID="Label161" runat="server" Text=' '> </asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TA_KW")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label21" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TA_AMPS")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label22" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SG_RUNNING_HRG")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SG_CLUTCH_AIR_PR")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"SG_LO_PRESS_Color").ToString()  %>'>
                                                                <asp:Label ID="Label168" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SG_LO_PRESS")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"SG_LO_TEMP_Color").ToString()  %>'>
                                                                <asp:Label ID="Label23" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SG_LO_TEMP")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 19px;">
                                                                <asp:Label ID="Label234" runat="server" Text=' '> </asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label24" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SG_SG_COND_BRG_TEMP1")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label25" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SG_SG_COND_BRG_TEMP2")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label30" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SG_KW")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label34" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SG_AMPS")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </td>
                                            <td valign="top">
                                                <asp:Repeater ID="Repeater1" runat="server">
                                                    <HeaderTemplate>
                                                        <table cellspacing="0" cellpadding="3" rules="all" border="0" style="background-color: White;white-space:nowrap;
                                                            border-color: #efefef; width: 100%;">
                                                            <tr class="HeaderCellColor1">
                                                                <td colspan="7" align="center">
                                                                    TANK LEVEL
                                                                </td>
                                                            </tr>
                                                            <tr align="center" class="HeaderCellColor">
                                                                <td rowspan="2">
                                                                    Watch
                                                                </td>
                                                                <td rowspan="2">
                                                                    <label id="Label192" class="verticaltext">
                                                                        CYL. OIL Day Tk
                                                                    </label>
                                                                </td>
                                                                <td rowspan="2">
                                                                    <label id="Label1" class="verticaltext">
                                                                        M/E Sump
                                                                    </label>
                                                                </td>
                                                                <td colspan="2">
                                                                    Heavy Oil
                                                                </td>
                                                                <td rowspan="2">
                                                                    <label id="Label6" class="verticaltext">
                                                                        Blended Oil
                                                                    </label>
                                                                </td>
                                                                <td rowspan="2">
                                                                    <label id="Label5" class="verticaltext">
                                                                        D. O. Serv Tk
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                            <tr align="center" class="HeaderCellColor">
                                                                <td>
                                                                    <label id="Label7" class="verticaltext2">
                                                                        Settl. Tk
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    <label id="Label8" class="verticaltext2">
                                                                        Serv Tk
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr align="center">
                                                            <td align="left" style="height: 18px;width:125px">
                                                                <asp:Label ID="Label159g" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LOG_WATCH")%>'> </asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label234" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CYL_OIL_DAY_TK")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label235" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_SUMP")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label236" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HEAVY_OIL_SETTL_TK")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HEAVY_OIL_SERV_TK")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BELENDED_OIL")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DO_SERV_TK")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr style="background-color: #efefcf; border: 1px solid ActiveBorder" align="center">
                                                            <td align="left" style="height: 18px;">
                                                                <asp:Label ID="Label159h" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LOG_WATCH")%>'> </asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label234" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CYL_OIL_DAY_TK")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label235" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ME_SUMP")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label236" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HEAVY_OIL_SETTL_TK")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HEAVY_OIL_SERV_TK")%>'></asp:Label>
                                                            </td>
                                                            <td class="CellClass0">
                                                                <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BELENDED_OIL")%>'></asp:Label>
                                                            </td>
                                                            <td style="height: 18px;" class="CellClass0">
                                                                <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DO_SERV_TK")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                                <asp:Repeater ID="Repeater3" runat="server">
                                                    <HeaderTemplate>
                                                        <table cellspacing="0" cellpadding="3" rules="all" border="0" style="background-color: White;white-space:nowrap;
                                                            border-color: #efefef; width: 100%; height: 100%">
                                                            <tr align="center" class="HeaderCellColor">
                                                                <td>
                                                                    Watch
                                                                </td>
                                                                <td>
                                                                    REMARKS OF ENGINEER OFFICER ON WATCH
                                                                </td>
                                                                <td>
                                                                    SING/ RANK
                                                                </td>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td align="left" style="width:50px"> 
                                                                <asp:Label ID="Label159i" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LOG_WATCH")%>'> </asp:Label>
                                                            </td>
                                                            <td align="left" height="50px" >
                                                                <asp:Label ID="Label235" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ENG_OFF_REMARKS")%>'> </asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="Label236" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ENG_OFF_RANK")%>'> </asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label159j" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LOG_WATCH")%>'> </asp:Label>
                                                            </td>
                                                            <td align="left" height="50px">
                                                                <asp:Label ID="Label235" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ENG_OFF_REMARKS")%>'> </asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="Label236" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ENG_OFF_RANK")%>'> </asp:Label>
                                                            </td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </td>
                                            <td width="20%" rowspan="3" valign="top">
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                            
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="50%" valign="top">
                                                
                                            </td>
                                            <td valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                            </td>
                                            <td rowspan="2" valign="top">
                                            </td>
                                            <td width="20%" rowspan="2" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                            </td>
                                        </tr>
                                      
                                    </table>
                          <%--      </ItemTemplate>
                            </asp:FormView>--%>
                        </td>
                    </tr>
                </table>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
 

         
    </div>
     
</center>
</asp:Content>
