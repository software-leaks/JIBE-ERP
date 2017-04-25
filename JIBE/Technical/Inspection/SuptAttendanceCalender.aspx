<%@ Page Title="Suprintendent Inspection Attendance" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="SuptAttendanceCalender.aspx.cs" Inherits="Technical_Worklist_SuptAttendanceCalender" ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../Scripts/jquery.min1.8.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .page
        {
            width: 100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }
        .CurrentNew
        {
            background-color: #999999;
            color: White;
            text-align: center;
            font-size: 12px;
            font-weight: bold;
            font-family: Tahoma;
        }
        
          .DateStyle
        {
            background-color: #ADEAFF;
            color: Black;
            max-width:35px;
            min-width:35px;
            height:35px;
            border: 1px solid #E6E6E6;
            border-collapse: collapse;
            font-size: 12px;
            font-weight: bold;
            font-family: Tahoma;
            text-align: center;
              border-color:Gray;
        }
        .DayStyle
        {
            background-color: #ADEAFF;
            color: Black;
            border: 1px solid #E6E6E6;
            border-collapse: collapse;
            font-size: 12px;
            font-weight: bold;
            font-family: Tahoma;
            text-align: center;
            border-color:Gray;
        }
        .NormStyle
        {
            min-width:35px;
            max-width:35px;
          font-size:11px;
          font-family:Tahoma;
            height:35px;
            border: 1px solid #E6E6E6;
            border-collapse: collapse;
            border-color:Gray;
            white-space:normal;
           
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
            background: url(../../Images/gridheaderbg-silver-image.png) left -0px repeat-x;
            background-color: #9575CD;
            font-weight: bold;
            color: Black;
            border: 1px solid darkgrey;
           border-color:Gray;
            border-collapse: collapse;
            padding:5px;
        }
        .SupStyle
        {
             text-align:center;
             color: #333333;
            min-width:175px;
            max-width:175px;
             font-size:12px; 
             font-family:Tahoma;
             font-weight:bold;
             border: 1px solid #E6E6E6;
               border-color:Gray;
            border-collapse: collapse;
           
            
        }
        .VesselStyle
        {
            
           
            color: #333333;
            font-size:12px; 
            font-family:Tahoma;
            min-width:175px;
            max-width:175px;
               
            border: 1px solid #E6E6E6;
            border-collapse: collapse;
            border-color:Gray;  
             
        }
    </style>
    <script type="text/javascript">

        function GetDivData() {



            var str = document.getElementById('dvMain').innerHTML;
            document.getElementById($('[id$=hdnContent]').attr('id')).value = str;
            //        __dopostback("BtnPrintPDF", "onclick")
            __doPostBack("<%=BtnPrintPDF.UniqueID %>", "onclick");



        }


        function LoadCalendar(pUserCompanyId, pStartDate) {
            if (lastExecutor != null)
                lastExecutor.abort();
            var service = Sys.Net.WebServiceProxy.invoke('/' + __app_name + '/JibeWebServiceInspection.asmx', 'asncLoadCalendarBySupt', false, { "pUserCompanyId": pUserCompanyId, "pStartDate": pStartDate }, OnSuccGet_Table, Onfail, new Array(""));
            lastExecutor = service.get_executor();

        }
        var year;
        var month;
        var pFilterCompany = "";

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


        var lTempDate;
        var lasty;
        var lastm;
        function Current() {
            var lcomid = pFilterCompany;
            lTempDate = new Date();
            LoadCalendar(lcomid, lTempDate);
        }


        function Next() {

            var lcomid = pFilterCompany;
            lTempDate = lTempDate.addMonths(1);
            LoadCalendar(lcomid, lTempDate);
        }

        function LastLoad() {
            var lcomid = pFilterCompany;
            LoadCalendar(lcomid, lTempDate);
        }


        function Prev() {
            var lcomid = pFilterCompany;
            lTempDate = lTempDate.addMonths(-1);
            LoadCalendar(lcomid, lTempDate);
        }

        function OnSuccGet_Table(retval, prm) {
            try {


                document.getElementById("ctl00_MainContent_lblSelection").innerHTML = retval[1];
                var cont = retval[0];
                var cont0 = retval[2];
                $("#newt").html(cont0);
                $("#newt0").html(cont);



            }
            catch (ex)
    { }
        }
        function Onfail(retval) {

        }
        function getCompany() {
            var DropdownList = document.getElementById( $('[id$=DDLCompany]').attr('id'));

            pFilterCompany = DropdownList.value;
            Current();


        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="page-title">
        Suprintendent Inspection Attendance
    </div>
    <div>
        <table style="width: 100%;" align="right">
            <tr>
                <td >
                    <asp:Label ID="lblCompany" runat="server" Text="Company: " Font-Size="12px" Font-Names="Tahoma" Font-Bold="true" ForeColor="Black" ></asp:Label>
                   
                </td>
                <td>
                     <asp:DropDownList ID="DDLCompany" runat="server" UseInHeader="false" 
                        AutoPostBack="true" Width="130" Style="margin-left: 0px" OnSelectedIndexChanged="DDLCompany_SelectedIndexChanged"/>

                       <%-- OnSelectedIndexChanged="DDLCompany_SelectedIndexChanged"--%>
                </td>
              
                <td style="width: 100%;" align="right">
                    <asp:Button ID="BtnPrevious" runat="server" Text="<" Width="30px" Height="30px" OnClick="BtnPrevious_Click"
                        Font-Bold="True" ToolTip="Previous Month" />
                    <asp:Button ID="BtnCurrent" runat="server" Text="Current" Height="30px" OnClick="BtnCurrent_Click"
                        Font-Bold="True" ToolTip="Current Month" />
                    <asp:Button ID="BtnNext" runat="server" Text=">" Width="30px" Height="30px" OnClick="BtnNext_Click"
                        Font-Bold="True" ToolTip="Next Month" />
                    &nbsp;&nbsp;
                    <asp:ImageButton ID="BtnPrint" runat="server" OnClientClick="return PrintDiv('dvMain');"
                        ImageUrl="~/Images/Printer.png" Width="20px" Height="20px" ToolTip="Print"></asp:ImageButton>
                </td>
                <td style="width: 2%; vertical-align: top;" align="right">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:ImageButton ID="BtnPrintPDF" runat="server" ImageUrl="~/Images/PDF-icon.png"
                                Width="20px" Height="20px" ToolTip="Export PDF" OnClick="BtnPrintPDF_Click" OnClientClick="GetDivData()">
                            </asp:ImageButton>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdnContent" runat="server" />
    </div>
    <div id="dvMain" align="center">
      <link href="../../Styles/crew_css.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
            .page
            {
                width: 1440px;
                background-color: #fff;
                margin: 5px auto 0px auto;
                border: 1px solid #496077;
            }
            .CurrentNew
            {
                background-color: #999999;
                color: Black;
                text-align: center;
                font-size: 12px;
                font-weight: bold;
                font-family: Tahoma;
            }
             .Summary-FooterStyle-css
            {
                /* background: url(../../Images/gridheaderbg-silver-image.png) left -0px repeat-x;*/
                color: #333333;
                background-color: #fff;
                font-weight: bold;
                font-size: 12px;
                font-family:Tahoma;
                border: 1px solid #000;
            }
            .Summary-FooterStyle-css td
            {
                border: 1px solid #000;
                border-collapse: collapse;
            }
            .Summary-HeaderStyle-css
            {
                /* background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
                color: #333333;
                font-size: 12px;
                font-family:Tahoma;
                padding: 5px;
                text-align: center;
                vertical-align: middle;
                background-color: #CCCCCC;
                border: 1px solid #000;
                border-collapse: collapse;
            }
            .Summary-HeaderStyle-css th
            {
                border: 1px solid #000;
                border-collapse: collapse;
            }
            .Summary-SubHeaderStyle-css
            {
                /* background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
                color: #fff;
                   font-size: 12px;
                font-family:Tahoma;
                padding: 5px;
                text-align: center;
                vertical-align: middle;
                background-color: #333333;
                border: 1px solid #000; /* border-collapse: collapse;*/
            }
            .Summary-SubHeaderStyle-css th
            {
                border: 1px solid #000; /* border-collapse: collapse;*/
            }
             
            
              
           .DateStyle
        {
            background-color: #ADEAFF;
            color: Black;
            max-width:35px;
            min-width:35px;
            height:35px;
            border: 1px solid #E6E6E6;
            border-collapse: collapse;
            font-size: 12px;
            font-weight: bold;
            font-family: Tahoma;
            text-align: center;
              border-color:Gray;
        }
        .DayStyle
        {
            background-color: #ADEAFF;
            color: Black;
            border: 1px solid #E6E6E6;
            border-collapse: collapse;
            font-size: 12px;
            font-weight: bold;
            font-family: Tahoma;
            text-align: center;
            border-color:Gray;
        }
        .NormStyle
        {
            min-width:35px;
            max-width:35px;
          font-size:11px;
          font-family:Tahoma;
            height:35px;
            border: 1px solid #E6E6E6;
            border-collapse: collapse;
            border-color:Gray;
            white-space:normal;
           
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
            background: url(../../Images/gridheaderbg-silver-image.png) left -0px repeat-x;
            background-color: #9575CD;
            font-weight: bold;
            color: Black;
            border: 1px solid darkgrey;
           border-color:Gray;
            border-collapse: collapse;
            padding:5px;
        }
        .SupStyle
        {
             text-align:center;
             color: #333333;
            min-width:175px;
            max-width:175px;
             font-size:12px; 
             font-family:Tahoma;
             font-weight:bold;
             border: 1px solid #E6E6E6;
               border-color:Gray;
            border-collapse: collapse;
           
            
        }
        .VesselStyle
        {
            
           
            color: #333333;
            font-size:12px; 
            font-family:Tahoma;
            min-width:175px;
            max-width:175px;
               
            border: 1px solid #E6E6E6;
            border-collapse: collapse;
            border-color:Gray;  
    </style>
       
        <table style="width:1430px;">
            <tr>
                <td  align="center">
                    <asp:Image ID="imgLogo" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <div style="font-size: 16px; font-weight: bold; font-family: Tahoma;" align="center">
                        ATTENDANCES
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="float: left; font-size: 16px; font-weight: bold; font-family: Tahoma;">
                        <asp:Label ID="lblMonthYear" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="float: right; font-size: 16px; font-weight: bold; font-family: Tahoma;">
                        DATE:<asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div >
                    
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                            <ContentTemplate>
                                <asp:GridView ID="grdSupAtt" runat="server" AllowSorting="false" AutoGenerateColumns="true"
                                    CaptionAlign="Bottom" CellPadding="2" EmptyDataText="No Record Found" BorderStyle="Solid"
                                    ForeColor="#333333" GridLines="Both" OnRowDataBound="grdSupAtt_RowDataBound"
                                    Width="100%" BorderColor="#CCCCCC" CssClass="grid">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css"  />
                                    <RowStyle CssClass="RowStyle-css" Font-Names="Tahoma" Font-Size="14px"  />
                                    <HeaderStyle CssClass="Summary-HeaderStyle-css" Height="35px" HorizontalAlign="Center" />
                                    <Columns>
                                    </Columns>
                                    <EditRowStyle BackColor="#58FA82" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle ForeColor="#333333" Height="30px" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div id="newt" style="text-align: left; width: 100%; overflow-x: hidden;" align="center" runat="server">
                                </div>
                                <div id="newt0" style="text-align: left; width: 100%; height: 500px; overflow-x: hidden;"
                                    runat="server">
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                </td>
            </tr>
        </table>
    </div>

   

  
             
</asp:Content>
