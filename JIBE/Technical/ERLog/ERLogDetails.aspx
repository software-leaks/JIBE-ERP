<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="ERLogDetails.aspx.cs"
    Inherits="Technical_ERLog_ERLogDetails" Title="Engine Room Log Book" EnableEventValidation="false"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Deck_Engine_LogBook.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
     <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <script type="text/javascript">
        function ViewEdit(Id) {
            var value = document.getElementById('<%=lblLogId.ClientID%>').value;
            var VesselId = document.getElementById('<%=lblVesselId.ClientID%>').value;
            window.open("ERLogThreshold.aspx?ViewID=" + Id + "&LOGID=" + value + "&VESSELID=" + VesselId, "Test", "", "");
        }
        function ViewFunction(Id) {
            var value = document.getElementById('<%=lblLogId.ClientID%>').value;
            var VesselId = document.getElementById('<%=lblVesselId.ClientID%>').value;
            window.open(Id + "?LOGID=" + value + "&VESSELID=" + VesselId, "Test", "", "");
        }

        function closeReworkPopup() {
            hideModal('dvRework');
            alert('Rework request has been sent successfully to Vessel!');
        }
        function GetDivData() {
            var cell = document.getElementsByClassName('CellClass1');
            for (var i = 0; i < cell.length; i++) {

                cell[i].className = "CellClass0";

                if (cell.length >= 0) {
                    var cell = document.getElementsByClassName('CellClass1');
                    i = -1;
                }

            }
            var img = document.getElementsByClassName('transactLog');

            for (var i = 0; i < img.length; i++) {

                img[i].src = document.getElementById($('[id$=hdnBaseURL]').attr('id')).value + "Uploads/CrewImages/" + img[i].nameProp;



            }

            $("a").contents().unwrap()

            document.getElementById($('[id$=lblHead]').attr('id')).textContent = "ENGINE ROOM LOG BOOK"
            var str = document.getElementById('dvContent').innerHTML;
            document.getElementById($('[id$=lblHead]').attr('id')).textContent = "";
            document.getElementById($('[id$=hdnContent]').attr('id')).value = str;
            //        __dopostback("BtnPrintPDF", "onclick")
            __doPostBack("<%=BtnPrintPDF.UniqueID %>", "onclick");


        }

 
    </script>
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
            //        var conts = document.querySelectorAll(".doubleScroll");
            //        for (var i = 0; i < conts.length; i++)
            //            DoubleScroll(conts[i]);
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
        
        
        
        border-right:1px solid #cccccc;
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <div style="border: 1px solid #cccccc; width: 95%; height: 100%;">
            
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
            <asp:UpdatePanel ID="UpdatePanelERLOG" runat="server" >
            <ContentTemplate>
             
                <div id="page-header" class="page-title">
                    <b>ENGINE ROOM LOG BOOK</b>
                </div>
                  <div style="float:right;">
            <table>
            <tr>
            <td align="center" >
                            <asp:Label ID="lblMV" runat="server" Visible="false"> </asp:Label>
                            <asp:Button ID="btnThreshold" runat="server" Text="View/Add Threshold" OnClientClick="ViewFunction('ERLogBookThresHold.aspx');" />
                        </td>
                        <td id="versionrowlbl" runat="server" align="center">
                         <asp:Label ID="lblVersion" runat="server" Text="History:"  > </asp:Label>
                        </td>
                        <td id="versionrow" runat="server" align="center">
                           <asp:DropDownList ID="DDLVersion" runat="server" UseInHeader="false" 
                                AutoPostBack="true" Width="200" 
                                onselectedindexchanged="DDLVersion_SelectedIndexChanged" /></td>
                        <td align="center">
                         <asp:Button ID="btnRework" runat="server" Text="Rework Report" 
                                onclick="btnRework_Click"  />
                        </td>
                        <td>
                            <asp:ImageButton ID="BtnPrintPDF" runat="server" OnClientClick="GetDivData();"
                            ImageUrl="~/Images/PDF-icon.png"  Height="25px" ToolTip="Print" 
                                OnClick="BtnPrintPDF_Click"></asp:ImageButton>
                        </td>
                        </tr>
            </table>
            </div>
            <br />
            <br />
            <br />
              <div id="dvContent">
         
            <div style="text-align:center;">
             <asp:Label ID="lblHead" runat="server" Text="" Font-Size="32px" Font-Names="Tahoma"></asp:Label>
             </div>
                 <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                ">
                <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                    border-color: black; width: 100%; border-collapse: collapse;">
                    <tr>
                        <td width="6%;">
                            Vessel:
                        </td>
                        <td align="left">
                            <asp:Label ID="lblVesselName" runat="server"> </asp:Label>
                            <asp:TextBox ID="lblVesselId" Width="0px" runat="server"></asp:TextBox>

                        </td>
                        <td width="6%;">
                            Date:
                        </td>
                        <td align="center" width="10%">
                            <asp:Label ID="lblDate" runat="server"> </asp:Label>
                            <%-- <asp:Button ID="btnPrint" runat="server" Text="Print" Width="120px" OnClientClick="javascript:printDiv('dvPageContent')" />      --%>
                        </td>
                        <td width="6%;">
                            FROM :
                        </td>
                        <td align="center" width="10%">
                            <asp:Label ID="lblfrom" runat="server"> </asp:Label>
                            <asp:TextBox ID="lblLogId" Width="0px" runat="server"></asp:TextBox>
                        </td>
                        <td width="6%;">
                            To:
                        </td>
                        <td align="center" width="10%">
                            <asp:Label ID="lblTo" runat="server"> </asp:Label>
                        </td>
                      
                    </tr>
                </table>
            </div>
            <div style="text-align: left; overflow: scroll" id="dvPageContent" class="page-content-main doublescroll">


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
        
        
        
        border-right:1px solid #cccccc;
        
    </style>
                <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                    border-color:black; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                    <tr>
                        <td style="width: 100%; border-right: solid; border-right-color: black; border-right-width: 1px"
                            align="left" valign="top">
                            <asp:FormView ID="FormView1" runat="server" Width="100%" BorderWidth="0px" Font-Size="Small" Font-Names="Tahoma"
                                OnDataBound="FormView1_DataBound">
                                <RowStyle CssClass="PMSGridRowStyle-css" Height="18" />
                                <ItemTemplate>
                                    <table cellspacing="0" cellpadding="3" border="0.5" style="background-color: White;
                                        border-color: black; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                                        <tr>
                                            <td width="100%" colspan="3" valign="top">
                                                <asp:Repeater ID="rpEngine1" runat="server">
                                                    <HeaderTemplate>
                                                        <table cellspacing="1" cellpadding="3" rules="all" border="0" style="background-color: White;
                                                            border-color: black; width: 100%;">
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
                                                        <tr align="center" style="background-color: #efefcf; border: 1px solid black">
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
                                                        <table cellspacing="1" cellpadding="3" rules="all" border="0" style="background-color: White;
                                                            border-color: black; width: 100%;">
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
                                                        <tr style="background-color: #efefcf; border: 1px solid black" align="center">
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
                                                        <table cellspacing="1" cellpadding="3" rules="all" border="0" style="background-color: White;
                                                            border-color: black; width: 100%;">
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
                                                        <tr style="background-color: #efefcf; border: 1px solid black" align="center">
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
                                                <table cellspacing="0" cellpadding="3" rules="all" height="300px" border="0" style="background-color: White;
                                                    border-color: black; width: 100%; border-collapse: collapse;">
                                                    <tr align="center" class="HeaderCellColor1">
                                                        <td colspan="6" align="center" style="font-weight: bold">
                                                            BOILER/ COOLING WATER TEST
                                                        </td>
                                                    </tr>
                                                    <tr align="center" class="HeaderCellColor">
                                                        <td rowspan="2">
                                                        </td>
                                                        <td rowspan="2">
                                                            BLR
                                                        </td>
                                                        <td colspan="2">
                                                            M/E
                                                        </td>
                                                        <td rowspan="2">
                                                            A/E
                                                        </td>
                                                        <td rowspan="2">
                                                            COMPR
                                                        </td>
                                                    </tr>
                                                    <tr align="center" class="HeaderCellColor">
                                                        <td>
                                                            JKT
                                                        </td>
                                                        <td>
                                                            PIST
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td>
                                                            CHLORIDES/<br />
                                                            CONDUCTIVITY
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("BLR_CW_CHLORIDES_BLR") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("BLR_CW_CHLORIDES_MEJW") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label49" runat="server" Text='<%# Bind("BLR_CW_CHLORIDES_MEPW") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label50" runat="server" Text='<%# Bind("BLR_CW_CHLORIDES_AE") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label51" runat="server" Text='<%# Bind("BLR_CW_CHLORIDES_CMPR") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #efefcf; border: 1px solid black" align="center">
                                                        <td>
                                                            ALKALINITY
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label53" runat="server" Text='<%# Bind("BLR_CW_ALKALINITY_BLR") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label54" runat="server" Text='<%# Bind("BLR_CW_ALKALINITY_MEJW") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label55" runat="server" Text='<%# Bind("BLR_CW_ALKALINITY_MEPW") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("BLR_CW_ALKALINITY_AE") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("BLR_CW_ALKALINITY_CMPR") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td>
                                                            TOTAL
                                                            <br />
                                                            ALKALINITY
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label56" runat="server" Text='<%# Bind("BLR_CW_TALKALINITY_BLR") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label57" runat="server" Text='<%# Bind("BLR_CW_TALKALINITY_MEJW") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label58" runat="server" Text='<%# Bind("BLR_CW_TALKALINITY_MEPW") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("BLR_CW_TALKALINITY_AE") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("BLR_CW_TALKALINITY_CMPR") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #efefcf; border: 1px solid black" align="center">
                                                        <td>
                                                            PHOSPHATES
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label59" runat="server" Text='<%# Bind("BLR_CW_PHOSPHATES_BLR") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label60" runat="server" Text='<%# Bind("BLR_CW_PHOSPHATES_MEJW") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label61" runat="server" Text='<%# Bind("BLR_CW_PHOSPHATES_MEPW") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("BLR_CW_PHOSPHATES_AE") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("BLR_CW_PHOSPHATES_CMPR") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td>
                                                            BLOWDOWN
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label62" runat="server" Text='<%# Bind("BLR_CW_BLOWDOWN_BLR") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label63" runat="server" Text='<%# Bind("BLR_CW_BLOWDOWN_MEJW") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label64" runat="server" Text='<%# Bind("BLR_CW_BLOWDOWN_MEPW") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label9" runat="server" Text='<%# Bind("BLR_CW_BLOWDOWN_AE") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label10" runat="server" Text='<%# Bind("BLR_CW_BLOWDOWN_CMPR") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td>
                                                            NITRITES
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label180" runat="server" Text='<%# Bind("BLR_CW_NITRITES_BLR") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label181" runat="server" Text='<%# Bind("BLR_CW_NITRITES_MEJW") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label182" runat="server" Text='<%# Bind("BLR_CW_NITRITES_MEPW") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label183" runat="server" Text='<%# Bind("BLR_CW_NITRITES_AE") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label184" runat="server" Text='<%# Bind("BLR_CW_NITRITES_CMPR") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td>
                                                            DOSAGE
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label95" runat="server" Text='<%# Bind("BLR_CW_DOSAGE_BLR") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label96" runat="server" Text='<%# Bind("BLR_CW_DOSAGE_MEJW") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label97" runat="server" Text='<%# Bind("BLR_CW_DOSAGE_MEPW") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label13" runat="server" Text='<%# Bind("BLR_CW_DOSAGE_AE") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label14" runat="server" Text='<%# Bind("BLR_CW_DOSAGE_CMPR") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="50%" rowspan="2" valign="top">
                                                <asp:Repeater ID="rpTrainingDetails" runat="server">
                                                    <HeaderTemplate>
                                                        <table cellspacing="1" cellpadding="3" rules="all" border="0" style="background-color: White;
                                                            border-color: black; width: 100%;">
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
                                                            <td align="left" rowspan="4" style="height: 18px;">
                                                                <asp:Label ID="Label159f" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LOG_WATCH")%>'> </asp:Label>
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
                                                        <tr style="background-color: #efefcf; border: 1px solid black" align="center">
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
                                                        <tr style="background-color: #efefcf; border: 1px solid black" align="center">
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
                                            </td>
                                            <td valign="top">
                                                <asp:Repeater ID="Repeater1" runat="server">
                                                    <HeaderTemplate>
                                                        <table cellspacing="0" cellpadding="3" rules="all" border="0" style="background-color: White;
                                                            border-color: black; width: 100%;">
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
                                                            <td align="left" style="height: 18px;">
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
                                                        <tr style="background-color: #efefcf; border: 1px solid black" align="center">
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
                                            </td>
                                            <td width="20%" rowspan="3" valign="top">
                                                <asp:Repeater ID="Repeater3" runat="server">
                                                    <HeaderTemplate>
                                                        <table cellspacing="0" cellpadding="3" rules="all" border="0" style="background-color: White;
                                                            border-color: black; width: 100%; height: 100%">
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
                                                            <td align="left">
                                                                <asp:Label ID="Label159i" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LOG_WATCH")%>'> </asp:Label>
                                                            </td>
                                                            <td align="left" height="140px">
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
                                                            <td align="left" height="140px">
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
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <table cellspacing="0" cellpadding="3" rules="all" border="0" height="340px" style="background-color: White;
                                                    border-color:black; width: 100%; border-collapse: collapse;">
                                                    <tr class="HeaderCellColor1">
                                                        <td colspan="5" align="center" style="font-weight: bold">
                                                            FUEL OIL DAILY ACCOUNT (MT)
                                                        </td>
                                                    </tr>
                                                    <tr class="HeaderCellColor">
                                                        <td colspan="2">
                                                        </td>
                                                        <td>
                                                            H. O.
                                                        </td>
                                                        <td>
                                                            D. O.
                                                        </td>
                                                        <td>
                                                            G. O.
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #efefcf; border: 1px solid black">
                                                        <td colspan="2">
                                                            ROB, Prev. Noon
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label84" runat="server" Text='<%# Bind("FODA_HO_ROB_PNN") %>'></asp:Label>
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label85" runat="server" Text='<%# Bind("FODA_DO_ROB_PNN") %>'></asp:Label>
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label86" runat="server" Text='<%# Bind("FODA_GO_ROB_PNN") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td rowspan="7" class="HeaderCellColor">
                                                            <asp:Label ID="Label52" runat="server" class="verticaltext"> CONSUMPTIONS </asp:Label>
                                                        </td>
                                                        <td>
                                                            Mani Engine
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label87" runat="server" Text='<%# Bind("FODA_HO_CONS_ME") %>'></asp:Label>
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label88" runat="server" Text='<%# Bind("FODA_DO_CONS_ME") %>'></asp:Label>
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label89" runat="server" Text='<%# Bind("FODA_GO_CONS_ME") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #efefcf; border: 1px solid black">
                                                        <td>
                                                            Aux Engine
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label90" runat="server" Text='<%# Bind("FODA_HO_CONS_AE") %>'></asp:Label>
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label91" runat="server" Text='<%# Bind("FODA_DO_CONS_AE") %>'></asp:Label>
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label92" runat="server" Text='<%# Bind("FODA_GO_CONS_AE") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Boiler
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label93" runat="server" Text='<%# Bind("FODA_HO_CONS_BLR") %>'></asp:Label>
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label94" runat="server" Text='<%# Bind("FODA_DO_CONS_BLR") %>'></asp:Label>
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label98" runat="server" Text='<%# Bind("FODA_GO_CONS_BLR") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #efefcf; border: 1px solid black">
                                                        <td>
                                                            Tk Clean
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label99" runat="server" Text='<%# Bind("FODA_HO_CONS_TC") %>'></asp:Label>
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label100" runat="server" Text='<%# Bind("FODA_DO_CONS_TC") %>'></asp:Label>
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label101" runat="server" Text='<%# Bind("FODA_GO_CONS_TC") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Heat'g/IG
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label102" runat="server" Text='<%# Bind("FODA_HO_CONS_HTG") %>'></asp:Label>
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label103" runat="server" Text='<%# Bind("FODA_DO_CONS_HTG") %>'></asp:Label>
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label104" runat="server" Text='<%# Bind("FODA_GO_CONS_HTG") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #efefcf; border: 1px solid black">
                                                        <td>
                                                            --
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label68" runat="server" Text=""> </asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label69" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label105" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Total
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label106" runat="server" Text='<%# Bind("FODA_HO_CONS_TTL") %>'></asp:Label>
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label107" runat="server" Text='<%# Bind("FODA_DO_CONS_TTL") %>'></asp:Label>
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label108" runat="server" Text='<%# Bind("FODA_GO_CONS_TTL") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #efefcf; border: 1px solid black">
                                                        <td colspan="2">
                                                            Received
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label109" runat="server" Text='<%# Bind("FODA_HO_RCVD") %>'></asp:Label>
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label110" runat="server" Text='<%# Bind("FODA_DO_RCVD") %>'></asp:Label>
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label111" runat="server" Text='<%# Bind("FODA_GO_RCVD") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            Amended
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label112" runat="server" Text='<%# Bind("FODA_HO_AMEND") %>'></asp:Label>
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label113" runat="server" Text='<%# Bind("FODA_DO_AMEND") %>'></asp:Label>
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label114" runat="server" Text='<%# Bind("FODA_GO_AMEND") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #efefcf; border: 1px solid black">
                                                        <td colspan="2">
                                                            ROB This Noon
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label115" runat="server" Text='<%# Bind("FODA_HO_ROB") %>'></asp:Label>
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label116" runat="server" Text='<%# Bind("FODA_DO_ROB") %>'></asp:Label>
                                                        </td>
                                                        <td align="center" class="CellClass0">
                                                            <asp:Label ID="Label117" runat="server" Text='<%# Bind("FODA_GO_ROB") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="50%" valign="top">
                                                <asp:Repeater ID="Repeater2" runat="server">
                                                    <HeaderTemplate>
                                                        <table cellspacing="0" cellpadding="3" rules="all" border="0" style="background-color: White;
                                                            border-color: black; width: 100%;">
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
                                                        <tr style="background-color: #efefcf; border: 1px solid black" align="center">
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
                                                <table cellspacing="0" cellpadding="3" rules="all" height="215px" border="0" style="background-color: White;
                                                    border-color: black; width: 100%; border-collapse: collapse;">
                                                    <tr class="HeaderCellColor1">
                                                        <td colspan="5" align="center" style="font-weight: bold">
                                                            FRESH WATER DAILY ACCOUNT
                                                        </td>
                                                    </tr>
                                                    <tr class="HeaderCellColor">
                                                        <td rowspan="2">
                                                        </td>
                                                        <td rowspan="2">
                                                            PORTABLE
                                                        </td>
                                                        <td colspan="2">
                                                            WASH
                                                        </td>
                                                        <td rowspan="2">
                                                            DISTL'D
                                                        </td>
                                                    </tr>
                                                    <tr class="HeaderCellColor">
                                                        <td>
                                                            PORT
                                                        </td>
                                                        <td>
                                                            STBD
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td align="left">
                                                            ROB Prev. Noon
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label35" runat="server" Text='<%# Bind("FWDA_POT_ROB_PNN") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label232" runat="server" Text='<%# Bind("FWDA_WASHP_ROB_PNN") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label258" runat="server" Text='<%# Bind("FWDA_WASHS_ROB_PNN") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label259" runat="server" Text='<%# Bind("FWDA_DISTL_ROB_PNN") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #efefcf; border: 1px solid black" align="center">
                                                        <td align="left">
                                                            Produced
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label260" runat="server" Text='<%# Bind("FWDA_POT_PROD") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label261" runat="server" Text='<%# Bind("FWDA_WASHP_PROD") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label262" runat="server" Text='<%# Bind("FWDA_WASHS_PROD") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label263" runat="server" Text='<%# Bind("FWDA_DISTL_PROD") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td align="left">
                                                            Received
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label264" runat="server" Text='<%# Bind("FWDA_POT_RCVD") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label265" runat="server" Text='<%# Bind("FWDA_WASHP_RCVD") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label266" runat="server" Text='<%# Bind("FWDA_WASHS_RCVD") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label267" runat="server" Text='<%# Bind("FWDA_DISTL_RCVD") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #efefcf; border: 1px solid black" align="center">
                                                        <td align="left">
                                                            Domastic Cons.
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label268" runat="server" Text='<%# Bind("FWDA_POT_CNSMP") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label269" runat="server" Text='<%# Bind("FWDA_WASHP_CNSMP") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label270" runat="server" Text='<%# Bind("FWDA_WASHS_CNSMP") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label271" runat="server" Text='<%# Bind("FWDA_DISTL_CNSMP") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            ..
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #efefcf; border: 1px solid black" align="center">
                                                        <td align="left">
                                                            ROB This Noon
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label276" runat="server" Text='<%# Bind("FWDA_POT_ROB") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label277" runat="server" Text='<%# Bind("FWDA_WASHP_ROB") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label278" runat="server" Text='<%# Bind("FWDA_WASHS_ROB") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label279" runat="server" Text='<%# Bind("FWDA_DISTL_ROB") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <table cellspacing="0" cellpadding="3" rules="all" border="0" style="background-color: White;
                                                    border-color: black; width: 100%; border-collapse: collapse;">
                                                    <tr class="HeaderCellColor1">
                                                        <td colspan="9" align="center" style="font-weight: bold">
                                                            WORKING HOURS
                                                        </td>
                                                    </tr>
                                                    <tr align="center" class="HeaderCellColor">
                                                        <td>
                                                        </td>
                                                        <td>
                                                            M/E
                                                        </td>
                                                        <td>
                                                            AE-1
                                                        </td>
                                                        <td>
                                                            AE-2
                                                        </td>
                                                        <td>
                                                            AE-3
                                                        </td>
                                                        <td>
                                                            AE-4
                                                        </td>
                                                        <td>
                                                            TA
                                                        </td>
                                                        <td>
                                                            SE
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            PREVIOUS
                                                        </td>
                                                        <td class='CellClass0'>
                                                            <asp:Label ID="Label118" runat="server" Text='<%# Bind("WRKHRS_ME_PREV") %>'></asp:Label>
                                                        </td>
                                                        <td class='CellClass0'>
                                                            <asp:Label ID="Label119" runat="server" Text='<%# Bind("WRKHRS_AE1_PREV") %>'></asp:Label>
                                                        </td>
                                                        <td class='CellClass0'>
                                                            <asp:Label ID="Label120" runat="server" Text='<%# Bind("WRKHRS_AE2_PREV") %>'></asp:Label>
                                                        </td>
                                                        <td class='CellClass0'>
                                                            <asp:Label ID="Label121" runat="server" Text='<%# Bind("WRKHRS_AE3_PREV") %>'></asp:Label>
                                                        </td>
                                                        <td class='CellClass0'>
                                                            <asp:Label ID="Label15" runat="server" Text='<%# Bind("WRKHRS_AE4_PREV") %>'></asp:Label>
                                                        </td>
                                                        <td class='CellClass0'>
                                                            <asp:Label ID="Label16" runat="server" Text='<%# Bind("WRKHRS_TA_PREV") %>'></asp:Label>
                                                        </td>
                                                        <td class='CellClass0'>
                                                            <asp:Label ID="Label78" runat="server" Text='<%# Bind("WRKHRS_SG_PREV") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label79" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #efefcf; border: 1px solid black" align="center">
                                                        <td align="left">
                                                            NOON TO NOON
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label17" runat="server" Text='<%# Bind("WRKHRS_ME_NN") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label18" runat="server" Text='<%# Bind("WRKHRS_AE1_NN") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label74" runat="server" Text='<%# Bind("WRKHRS_AE2_NN") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label81" runat="server" Text='<%# Bind("WRKHRS_AE3_NN") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label82" runat="server" Text='<%# Bind("WRKHRS_AE4_NN") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label83" runat="server" Text='<%# Bind("WRKHRS_TA_NN") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label122" runat="server" Text='<%# Bind("WRKHRS_SG_NN") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label123" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td align="left">
                                                            TOTAL
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label75" runat="server" Text='<%# Bind("WRKHRS_ME_TTL") %>'> </asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label76" runat="server" Text='<%# Bind("WRKHRS_AE1_TTL") %>'> </asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label77" runat="server" Text='<%# Bind("WRKHRS_AE2_TTL") %>'> </asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label124" runat="server" Text='<%# Bind("WRKHRS_AE3_TTL") %>'> </asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label125" runat="server" Text='<%# Bind("WRKHRS_AE4_TTL") %>'> </asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label126" runat="server" Text='<%# Bind("WRKHRS_TA_TTL") %>'> </asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label127" runat="server" Text='<%# Bind("WRKHRS_SG_TTL") %>'> </asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label128" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td rowspan="2" valign="top">
                                                <table cellspacing="0" cellpadding="3" rules="all" border="0" height="220px" style="background-color: White;
                                                    border-color: black; width: 100%; border-collapse: collapse;">
                                                    <tr class="HeaderCellColor1">
                                                        <td colspan="4" align="center" style="font-weight: bold">
                                                            LUBRICATING OIL DAILY ACCOUNT (Ltr)
                                                        </td>
                                                    </tr>
                                                    <tr class="HeaderCellColor">
                                                        <td>
                                                            Grade
                                                        </td>
                                                        <td>
                                                            MECC
                                                        </td>
                                                        <td>
                                                            MECYL
                                                        </td>
                                                        <td>
                                                            AECC
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td align="left">
                                                            ROB Prev Noon
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label129" runat="server" Text='<%# Bind("LODA_MECC_ROB_PNN") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label130" runat="server" Text='<%# Bind("LODA_MECYL_ROB_PNN") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label20" runat="server" Text='<%# Bind("LODA_AECC_ROB_PNN") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #efefcf; border: 1px solid black" align="center">
                                                        <td align="left">
                                                            Received
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label26" runat="server" Text='<%# Bind("LODA_MECC_RCVD") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label27" runat="server" Text='<%# Bind("LODA_MECYL_RCVD") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label29" runat="server" Text='<%# Bind("LODA_AECC_RCVD") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td align="left">
                                                            Consumed
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label31" runat="server" Text='<%# Bind("LODA_MECC_CNSMP") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label32" runat="server" Text='<%# Bind("LODA_MECYL_CNSMP") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label70" runat="server" Text='<%# Bind("LODA_AECC_CNSMP") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #efefcf; border: 1px solid black" align="center">
                                                        <td align="left">
                                                            ROB This noon
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label71" runat="server" Text='<%# Bind("LODA_MECC_ROB") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label72" runat="server" Text='<%# Bind("LODA_MECYL_ROB") %>'></asp:Label>
                                                        </td>
                                                        <td class="CellClass0">
                                                            <asp:Label ID="Label73" runat="server" Text='<%# Bind("LODA_AECC_ROB") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="20%" rowspan="2" valign="top">
                                                <table cellspacing="0" cellpadding="3" rules="all" border="0" style="background-color: White;
                                                    border-color:black; width: 100%; border-collapse: collapse;">
                                                    <tr align="center" class="HeaderCellColor">
                                                        <td colspan="4" align="center" style="font-weight: bold">
                                                            Chief Engineer's Remarks/
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" height="160px">
                                                            <asp:Label ID="Label175" Height="150px" Width="150px" runat="server" Text='<%# Bind("CE_REMARKS") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Date
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label173" runat="server" Width="90px" Text='<%# Bind("CREATED_DATE") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            Signature
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label174" runat="server" Width="90px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <table cellspacing="0" cellpadding="3" rules="all" border="0" style="background-color: White;
                                                    border-color:black; width: 100%; border-collapse: collapse;">
                                                    <tr class="HeaderCellColor">
                                                        <td colspan="13" align="center" style="font-weight: bold">
                                                            DAILY PERFORMANCE
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Wind Force
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label19" runat="server" Width="90px" Text='<%# Bind("DP_WIND_FORCE") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            Rel. Direction
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label33" runat="server" Width="90px" Text='<%# Bind("DP_WIND_DIR") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            Sea Cond
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label36" runat="server" Width="90px" Text='<%# Bind("DP_SEA_COND") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            Swell Height
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label37" runat="server" Width="90px" Text='<%# Bind("DP_SWELL") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            Rel. Direction
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label38" runat="server" Width="90px" Text='<%# Bind("DP_SWELL_DIR") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            Curr.
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label39" runat="server" Width="90px" Text='<%# Bind("DP_CURR") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #efefcf; border: 1px solid black">
                                                        <td>
                                                            REV. Noon to Noon
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label80" runat="server" Width="90px" Text='<%# Bind("DP_REVS_NTN") %>'></asp:Label>
                                                        </td>
                                                        <td colspan="2">
                                                            Engine Distance
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label131" runat="server" Width="90px" Text='<%# Bind("DP_ENG_DIST") %>'></asp:Label>
                                                        </td>
                                                        <td colspan="2">
                                                            Objerved Distance
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label132" runat="server" Width="90px" Text='<%# Bind("DP_OBS_DIST") %>'></asp:Label>
                                                        </td>
                                                        <td colspan="2">
                                                            Total Distance
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label133" runat="server" Width="90px" Text='<%# Bind("DP_TTL_DIST") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Hours Full Speed
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label41" runat="server" Width="90px" Text='<%# Bind("DP_HRS_FUL_SPD") %>'></asp:Label>
                                                        </td>
                                                        <td colspan="2">
                                                            Average RPM
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label42" runat="server" Width="90px" Text='<%# Bind("DP_AVG_RPM") %>'></asp:Label>
                                                        </td>
                                                        <td colspan="2">
                                                            Slip%
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label43" runat="server" Width="90px" Text='<%# Bind("DP_SLIP") %>'></asp:Label>
                                                        </td>
                                                        <td colspan="2">
                                                            Distance To Go
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label44" runat="server" Width="90px" Text='<%# Bind("DP_DIST_TOGO") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #efefcf; border: 1px solid black">
                                                        <td>
                                                            Hrs Received Spd
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label45" runat="server" Width="90px" Text='<%# Bind("DP_HRS_RED_SPD") %>'></asp:Label>
                                                        </td>
                                                        <td colspan="2">
                                                            Hours Speed
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label46" runat="server" Width="90px" Text='<%# Bind("DP_HRS_STOPD") %>'></asp:Label>
                                                        </td>
                                                        <td colspan="2">
                                                            Objerved Speed
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label47" runat="server" Width="90px" Text='<%# Bind("DP_OBS_SPD") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            ETA
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:Label ID="Label48" runat="server" Width="90px" Text='<%# Bind("NEXT_PORT") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td style='font-weight: bold; font-size: 12px; color: #092B4C; text-align: center'>
                                                            CE Engineer:
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="ImgCE" runat="server" ImageUrl='<%#"/jibe/Uploads/CrewImages/" +Eval("VSLCE_PHOTOURL") %>'
                                                                Height="40px" Width="40px" CssClass="transactLog" />
                                                        </td>
                                                        <td style="font-size: 11px; color: #0D3E6E; line-height: 20px; text-align: left">
                                                            <a style="font-size: 11px; color: #0D3E6E" target="_blank" href='<%# "/jibe/crew/CrewDetails.aspx?ID=" +  Eval("VSLCE_ID") %>'>
                                                                <%# Eval("VSLCE_FULLNAME") %>
                                                            </a>
                                                        </td>
                                                        <td style='width: 50px'>
                                                            &nbsp;
                                                        </td>
                                                        <td style='font-weight: bold; font-size: 12px; color: #092B4C; text-align: center'>
                                                            Second Engineer :
                                                        </td>
                                                       <td>
                                                            <asp:Image ID="Img2E" runat="server" src='<%# "/"+ System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() +"/Uploads/CrewImages/" + Eval("VSL2E_PHOTOURL") %>'
                                                                alt="" Height="40px" Width="40px" CssClass="transactLog" />
                                                        </td>
                                                        <td style="font-size: 11px; color: #0D3E6E; line-height: 20px; text-align: left">
                                                            <a style="font-size: 11px; color: #0D3E6E" target="_blank" href='<%# "/"+ System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() +"/crew/CrewDetails.aspx?ID="+ Eval("VSL2E_ID") %>'>
                                                                <%# Eval("VSL2E_FULLNAME") %>
                                                            </a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:FormView>
                        </td>
                    </tr>
                </table>
            </div>

            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="updatepanerework" runat="server">
            <ContentTemplate>
                <div id="dvRework" style="display: none; width: 40%" title="Reason for Rework">
                    <center>
                        <table border="0" cellpadding="2" cellspacing="2" width="100%">
                             
                            <tr>
                                <td align="right" style="width:86px">
                                   Description&nbsp;:&nbsp;
                                </td>
                                
                                <td align="left">
                                    <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Width="100%"
                                        Rows="6"   Height="84px"></asp:TextBox>
                                   
                                </td>
                                 <td align="left">
                                    &nbsp; &nbsp; &nbsp; &nbsp;
                                </td>
                            </tr>
                        </table>
                    </center>
                    <div style="margin-top: 20px; background-color: #d8d8d8; text-align: center">
                        <table width="100%">
                            <tr>
                                <td style="width: 10%">
                                </td>
                                <td>
                                    <asp:Button ID="btnSaveSaveRework" runat="server" Text="Ok"  Width="100px"
                                        onclick="btnSaveSaveRework_Click"   />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                           <asp:Button ID="btnClose" runat="server" Text="Close"  Width="100px" onclick="btnClose_Click"
                                           />
                                </td>
                            </tr>
                        </table>
                    </div>
                      <asp:HiddenField ID="hdnBaseURL" runat="server"></asp:HiddenField>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
      <asp:HiddenField ID="hdnContent" runat="server" />
        </div>
    </center>
</asp:Content>
