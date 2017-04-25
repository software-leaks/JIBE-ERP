<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="LMS_Drill_Calendar.aspx.cs"
    EnableViewState="true" ValidateRequest="false" Inherits="LMS_LMS_Drill_Calendar"
    Title="Drill Calendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
   
    <script type="text/javascript">


        var year;
        var month;


        Date.isLeapYear = function (year) {
            return (((year % 4 === 0) && (year % 100 !== 0)) || (year % 400 === 0));
        };

        Date.getDaysInMonth = function (year, month) {
            return [31, (Date.isLeapYear(year) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][month];
        };

        Date.prototype.isLeapYear = function () {
            return Date.isLeapYear(this.getFullYear());
        };

        Date.prototype.getDaysInMonth = function () {
            return Date.getDaysInMonth(this.getFullYear(), this.getMonth());
        };

        Date.prototype.addMonths = function (value) {
            var n = this.getDate();
            this.setDate(1);
            this.setMonth(this.getMonth() + value);
            this.setDate(Math.min(n, this.getDaysInMonth()));
            return this;
        };


        function abc(x, a, b) {
            if (x) {
                var m = "test";

                js_ShowToolTip(m, a, b);
            }

        }

        var SlotDic = {};

        $(document).ready(function () {

            SlotDic[1] = 1;
            SlotDic[2] = 1;
            SlotDic[3] = 1;
            SlotDic[4] = 4;
            SlotDic[5] = 4;
            SlotDic[6] = 4;
            SlotDic[7] = 7;
            SlotDic[8] = 7;
            SlotDic[9] = 7;
            SlotDic[10] = 10;
            SlotDic[11] = 10;
            SlotDic[12] = 10;
            Current();

        });


        function LoadCalendar(pStartDate, Vessel_ID) {
            if (lastExecutor != null)
                lastExecutor.abort();
            var service = Sys.Net.WebServiceProxy.invoke('/' + __app_name + '/JibeWebService.asmx', 'ayncLoadDrillCalendar', false, { "pStartDate": pStartDate, "Vessel_ID": Vessel_ID }, OnSuccGet_Table, Onfail, new Array(""));
            lastExecutor = service.get_executor();

        }
        var lTempDate;
        var lasty;
        var lastm;
        function Current(Vessel_ID) {

            lTempDate = new Date();
            LoadCalendar(lTempDate, Vessel_ID);
        }


        function Next(Vessel_ID) {


            lTempDate = lTempDate.addMonths(1);
            LoadCalendar(lTempDate, Vessel_ID);
        }

        function LastLoad(Vessel_ID) {

            LoadCalendar(lTempDate, Vessel_ID);
        }


        function Prev(Vessel_ID) {

            lTempDate = lTempDate.addMonths(-1);
            LoadCalendar(lTempDate, Vessel_ID);
        }

        function OnSuccGet_Table(retval, prm) {
            try {


                document.getElementById("ctl00_MainContent_lblSelection").innerHTML = retval[1];
                var cont = retval[0];
                var cont0 = retval[2];
                $("[id$=newt]").html(cont0);
                $("[id$=newt0]").html(cont);
                var a = document.getElementById('<%=hf0.ClientID%>');
                document.getElementById('<%=hf0.ClientID%>').value = lTempDate;


            }
            catch (ex)
    { }
        }

        var lastExecutor = null;
        function Get_InspInfo(UserId, Schedule_Date, VESSEL_NAME, lEndDate, evt, objthis) {

            if (lastExecutor != null)
                lastExecutor.abort();

            var service = Sys.Net.WebServiceProxy.invoke('/' + __app_name + '/JibeWebService.asmx', 'asyncGet_InspInfo', false, { "UserId": UserId, "Schedule_Date": Schedule_Date, "VESSEL_NAME": VESSEL_NAME, "lEndDate": lEndDate }, OnSuccGet_Record_Information, Onfail, new Array(evt, objthis));
            lastExecutor = service.get_executor();
        }
        function OnSuccGet_Record_Information(retval, prm) {
            try {
                js_ShowToolTip(retval, prm[0], prm[1]);
            }
            catch (ex)
    { }
        }
        function Onfail(retval) {

        }
 
    </script>
    <style type="text/css">
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="border: 1px solid #cccccc;">
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="updSupInspCal" runat="server">
            <ContentTemplate>
                <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                    height: 100%;">
                    <div id="page-header" class="page-title">
                        <b>Drill Calendar</b>
                    </div>
                    <table style="width: 100%;">
                        <tr>
                            <td style="font-size: 14px; font-weight: bold; text-align: left;width: 70%">
                            <asp:ImageButton ID="ImgExportToExcel" runat="server" AlternateText="Export" Height="22px" ToolTip="Export To Excel"
                                        OnClick="ImgExportToExcel_Click" src="../Images/DocTree/xls.jpg" />
                            </td>
                             <td style="font-weight:bold"> Vessel :
                            <td>
                            <td> <asp:DropDownList ID="DDLVessel" runat="server" UseInHeader="false"  AutoPostBack="true"
                                    Width="160" onselectedindexchanged="DDLVessel_SelectedIndexChanged" /></td>
                            <td>
                                <div style="text-align: right;">
                                <table>
                                <tr>
                              
                                <td>
                                          <asp:Button type="button" runat="server" id="MovePrev" Text=" < " 
                                              onclick="MovePrev_Click"   />
                                    <asp:Button runat="server" type="button" id="Cur" Text=" Current " 
                                              onclick="Cur_Click"  />
                                    <asp:Button runat="server" type="button" id="MoveNext" Text=" > " 
                                              onclick="MoveNext_Click"   /></td>
                                    
                                </tr>
                                </table>
                               
                                     
                                  
                                 
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div id="divexport" runat="server" enableviewstate="true">
                        <style type="text/css">
        .page
        {
            width: 1300px;
            overflow-y: hidden;
        }
        
        
        .MonthProgramCellSyle0
        {
            min-width: 15px;
            max-width: 15px;
            border: 1px solid #E6E6E6;
            border-collapse: collapse;
            max-width: 80px;
            min-width: 80px;
            padding: 5px;
        }
        .MonthProgramCellSyle1
        {
            min-width: 15px;
            max-width: 15px;
            border: 1px solid #E6E6E6;
            border-collapse: collapse;
            background-color: Green;
            color: Green;
            max-width: 80px;
            min-width: 80px;
            padding: 5px; 
        }
           
        .MonthProgramCellSyle2
        {
            min-width: 15px;
            max-width: 15px;
            border: 1px solid #E6E6E6;
            border-collapse: collapse;
            background-color: Red;
            color: Red;
            max-width: 80px;
            min-width: 80px;
            padding: 5px;
        }
          
        .MonthProgramCellSyle3
        {
            min-width: 15px;
            max-width: 15px;
            border: 1px solid #E6E6E6;
            border-collapse: collapse;
            background-color: Blue;
            color: Blue;
            max-width: 80px;
            min-width: 80px;
            padding: 5px;
        }
        
        #t00
        {
            padding: 0px;
            border-collapse: collapse;
        }
        #t01
        {
            padding: 0px;
            border-collapse: collapse;
        }
        .MonthStyle
        {
            background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x;
            background-color: #9575CD;
            font-weight: bold;
            color: Black;
            border: 1px solid darkgrey;
            border-collapse: collapse;
            padding: 5px;
            max-width: 80px;
            min-width: 80px;
        }
        .ProgramHeadStyle
        {
            background-color: green;
            min-width: 175px;
            max-width: 175px;
            color: White;
            border: 1px solid #E6E6E6;
            border-collapse: collapse;
            padding-left:2px;
        }
        .ProgramCellStyle
        {
            background-color: #F3F3F3;
            color: Black;
            min-width: 175px;
            max-width: 175px;
            border: 1px solid #E6E6E6;
            border-collapse: collapse;
            min-width: 175px;
            max-width: 175px;
            padding-left:2px;
        }
    </style>
                        <table style="width: 100%;">
                            <tr>
                                <td style="font-size: 14px; font-weight: bold; text-align: left;">
                                    <asp:Label ID="lblSelection" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="newt" style="text-align: left; overflow-x: hidden;" runat="server" enableviewstate="true">
                                    </div>
                                    <div id="newt0" style="text-align: left; height: 500px; overflow-y: scroll; overflow-x: hidden;"
                                        runat="server" enableviewstate="true">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:HiddenField ID="hdnStartMonth" runat="server" Value="" />
                    <asp:TextBox ID="hf0" runat="server" Style="display: none" />
                    <asp:TextBox ID="hf1" runat="server" Style="display: none" />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ImgExportToExcel" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
