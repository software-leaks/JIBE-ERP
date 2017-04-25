<%@ Page Title="Calender" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CalendarMain.aspx.cs" Inherits="QMS_Calendar_CalendarMain" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <%-- <link href="../../Styles/crew_css.css" rel="Stylesheet" type="text/css" />--%>
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js?" type="text/javascript"></script>
    <script src="../../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var isdivCalExists = "0";



        function ChangeColor(tableRow, highLight) {
            if (highLight) {
                tableRow.style.backgroundColor = '#dcfac9';
            }
            else {
                tableRow.style.backgroundColor = 'white';
            }
        }


        var TimeFrom = "";
        var RoomID = "";
        var SelDate = "";


        function NewBooking(cell) {


            if (isdivCalExists == "0") {
                //debugger;
                var strids = cell.toString().split('_');

                TimeFrom = strids[1].toString();
                RoomID = strids[2].toString();
                SelDate = $('[id$=hdfDate]').val();

                var currentdate = new Date();
                var selectedDate = Date.parse(SelDate);

                WebService.CheckValidBookingDateTime(SelDate, TimeFrom, TimeFrom, onCheckValidBookingDateTime);


            }
            isdivCalExists = "0";
        }

        function onCheckValidBookingDateTime(result) {

            if (result == "1") {
                alert('Rooms cannot be book in the past.');
            }
            else if (result == "2") {
                alert('You cannot book meeting room after 1800 hours.');
            }
            else {


                document.getElementById("iFrmBooking").src = 'CalendarBookingEntry.aspx?TimeFrom=' + TimeFrom + '&RoomID=' + RoomID + '&SelDate=' + SelDate;

                showModal('dvBookingPopUp', false);

                TimeFrom = "";
                RoomID = "";
                SelDate = "";
            }
        }

        function DatacellMouseOver(cell) {

            var strids = cell.toString().split('_');
            var TimeLineID = strids[1].toString();
            var RoomID = strids[2].toString();

            //remove selection
            $('.data-cell').css({ "border": "0", "border-bottom": "1px dashed #cccccc", "background-color": "White" });
            $('.MeetingRoom').css({ "background-color": "White" });
            $('.TimeLine').css({ "background-color": "White" });

            //show highlight color
            $('#' + RoomID).css({ "background-color": "Yellow" });
            $('#T' + TimeLineID).css({ "background-color": "Yellow" });
            $(document.getElementById(cell)).css({ "background-color": "#CEF0FF" });
        }


        function getBookings(CurrentUserID, bookingdate) {


            if (CurrentUserID != "") {
                // set label
                Async_getBookingList(CurrentUserID, bookingdate);
            }
        }


        function Async_getBookingList(userid, bookingdate) {
            var url = "../../webservice.asmx/getMeetingRoomBookingCalendar";
            var params = 'bookingdate=' + bookingdate + '&userid=' + userid;


            obj = new AsyncResponse(url, params, response_getBookingList);
            obj.getResponse();
        }



        function response_getBookingList(retval) {


            if (retval.indexOf('Working') >= 0) { return; }

            retval = clearXMLTags(retval);
            if (retval.indexOf('ERROR:', 0) >= 0 || retval.indexOf('error', 0) >= 0) {
                alert(retval);
                return;
            }




            if (retval.trim().length > 0) {
                var arVal = eval(retval);

                for (var i = 0; i < arVal.length; i++) {
                    var lstItem = arVal[i];
                    var tdid = "td_" + lstItem.TIME_FROM + "_R" + lstItem.ROOMID
                    var bookby = lstItem.BOOKING_BY;
                    var bookingon = lstItem.BOOKING_ON;
                    var timefrom = lstItem.TIME_FROM;
                    var timeto = lstItem.TIME_TO;
                    var tempdate = Date.parse(bookingon);
                    var timediff = lstItem.TIMEDIFF;
                    var timetitle = lstItem.TIME_FROM_24HRS + " To " + lstItem.TIME_TO_24HRS;
                    var Purpose = lstItem.SHORTREMARK;
                    var AcceptedBy = lstItem.AcceptedBy
                    var RejectedBy = lstItem.RejectedBy

                    // box height
                    var cal_td = document.getElementById(tdid);

                    var div1 = document.createElement('div');
                    div1.id = lstItem.ID;
                    div1.className = "booking-data-div";

                    var tab1 = document.createElement('table');
                    var tb = document.createElement('tbody');
                    tab1.className = "booking-data-table";

                    var th1 = appendRow(tb, 'fixed-header');
                    appendCell(th1, 'PIC :' + bookby + '-' + timetitle);
                    tb.appendChild(th1)

                    var th2 = appendRow(tb, 'fixed-header');
                    appendCell(th2, Purpose);
                    tb.appendChild(th2);

                    tab1.appendChild(tb);
                    div1.appendChild(tab1);

                    
                    $(div1).css({ "min-height": getBoxHeight(timediff) });


                    var tabImage = document.createElement('table');
                    tabImage.className = "Image-data-table";
                    var tbo = document.createElement('tbody');
                    var row, cell, iAccepted, iRejected;

                    row = document.createElement('tr');

                    cell = document.createElement('td');
                    cell.className = "Image-data-table-td";

                    var img = document.createElement('IMG');
                    img.className = "div-td-Image"
                    
                    if (AcceptedBy.length > 1) {
                        AcceptedBy = AcceptedBy.replace(new RegExp(',', 'g'), '<br>');
                        img.setAttribute("src", "../../Images/Accepted.png")
                        img.setAttribute("Title", "cssbody=[dvbdy2] cssheader=[dvhdr2] header=[Accepted By] body=[" + AcceptedBy + "]");
                        cell.appendChild(img);
                        iAccepted = 1
                    }
                                        
                    row.appendChild(cell);

                    var img = document.createElement('IMG');
                    img.className = "div-td-Image"

                    
                    if (RejectedBy != "") {
                        RejectedBy = RejectedBy.replace(new RegExp(',', 'g'), '<br>');
                        img.setAttribute("src", "../../Images/Rejected.png")
                        img.setAttribute("Title", "cssbody=[dvbdy2] cssheader=[dvhdr2] header=[Rejected By] body=[" + RejectedBy + "]");
                        cell.appendChild(img);
                        iRejected = 1
                    }
                    

                    if (iAccepted == 1 || iRejected == 1) {
                        row.appendChild(cell);
                        tbo.appendChild(row);
                        tabImage.appendChild(tbo);
                        div1.appendChild(tabImage);
                    }

                    
                    $(div1).click(function () {
                        var id = this.id;
                        dvBooking_Click(id)
                    });

                    cal_td.appendChild(div1);
                }

            }
        }



        function dvBooking_Click(booingid) {

            document.getElementById("iFrmBooking").src = 'CalendarBookingEntry.aspx?Bookingid=' + booingid;
            showModal('dvBookingPopUp', false);
            isdivCalExists = booingid;

        }

        function getBoxHeight(timediff) {

            var unitHeight = 30;
            var h = timediff * unitHeight;

            if (h > 0) {
                return h + 'px';
            }
            else {
                return unitHeight + 'px';
            }
        }
        function appendRow(tbody, cssClass) {
            try {
                var tr = document.createElement('tr');
                if (cssClass)
                    tr.className = cssClass;
                tbody.appendChild(tr);
            }
            catch (ex) { }
            return tr;
        }
        function appendCell(tr, html, title, cssClass) {
            try {
                var td = document.createElement('td');
                if (html)
                    td.innerHTML = html;

                if (title)
                    td.title = title;

                if (cssClass)
                    td.className = cssClass;

                tr.appendChild(td);
            }
            catch (ex) { }
            return td;
        }

        function RefreshFromchild() {
            location.reload(true);
        }
        
    </script>
    <style type="text/css">
        .highlight
        {
            background-color: Yellow;
        }
        .data-cell
        {
            min-height: 30px;
            width: 230px;
        }
        
        .table-css td
        {
            border-bottom: 1px dashed #CCCCCC;
            vertical-align: top;
        }
        .booking-data-table
        {
            padding: 0;
            width: 100%;
            
        }
        
        .booking-data-table tr
        {
            background-color: #66CCFF;
            color: Black;
        }
        .booking-data-table td
        {
            border: none;
        }
        .booking-data-div
        {
            position: absolute;
            border: 1px solid gray;
            background-color: #EFEFEF;
            width: 227px;
            text-align: left;
        }
        
        .Image-data-table
        {
            padding: 0;
            width: 100%;
        }
        .Image-data-table-td
        {
            text-align: center;
        }
        
        .div-td-Image
        {
            height: 16px; 
            margin-right: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <asp:ScriptManagerProxy ID="smp1" runat="server">
            <Services>
                <asp:ServiceReference Path="~/WebService.asmx" />
            </Services>
        </asp:ScriptManagerProxy>
        <div>
            <%-- <div style="border: 1px solid  #006699; padding: 2px; background-color: #5588BB; 
                color: #FFFFFF; text-align: center;">
                <b>Calender</b>
            </div>--%>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="left" style="width: 35%; vertical-align: top">
                        <asp:UpdatePanel runat="server" ID="updCalender" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div>
                                    <asp:HiddenField ID="hdfDate" runat="server" />
                                    <asp:Calendar ID="MeetingCal" runat="server" BackColor="#FFFFCC" BorderColor="White"
                                        BorderWidth="1px" Font-Names="Verdana" Font-Size="18px" ForeColor="Black" Height="450px"
                                        NextPrevFormat="FullMonth" Width="400px" OnDayRender="Calendar1_DayRender" OnSelectionChanged="MeetingCal_SelectionChanged">
                                        <DayHeaderStyle Font-Bold="True" Font-Size="11pt" />
                                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                        <OtherMonthDayStyle ForeColor="#999999" />
                                        <SelectedDayStyle BackColor="Green" ForeColor="White" />
                                        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="2px" Font-Bold="True"
                                            Font-Size="12pt" ForeColor="#333399" />
                                        <TodayDayStyle BackColor="#AEF09B" />
                                    </asp:Calendar>
                                </div>
                                <div>
                                    <center>
                                        <table>
                                            <tr>
                                                <td align="right">
                                                    Today : &nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:LinkButton ID="lbTodayDtDisplay" runat="server" OnClick="lbTodayDtDisplay_Click"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </center>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="vertical-align: top">
                        <asp:UpdatePanel runat="server" ID="updBooking" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div>
                                    <table cellpadding="1" cellspacing="1" width="100%" class="table-css">
                                        <tr>
                                            <td align="left">
                                                <b>Hrs.</b>
                                            </td>
                                            <td id="R1" class="MeetingRoom">
                                                <b>Board Room </b>
                                            </td>
                                            <td id="R2" class="MeetingRoom">
                                                <b>Crew Briefing Room </b>
                                            </td>
                                            <td id="R3" class="MeetingRoom">
                                                <b>Mgmt Meeting Room <b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T0000" class="TimeLine">
                                                0000
                                            </td>
                                            <td id="td_0000_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0000_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0000_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T0030" class="TimeLine">
                                                0030
                                            </td>
                                            <td id="td_0030_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0030_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0030_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T0100" class="TimeLine">
                                                0100
                                            </td>
                                            <td id="td_0100_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0100_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0100_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T0130" class="TimeLine">
                                                0130
                                            </td>
                                            <td id="td_0130_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0130_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0130_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T0200" class="TimeLine">
                                                0200
                                            </td>
                                            <td id="td_0200_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0200_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0200_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T0230" class="TimeLine">
                                                0230
                                            </td>
                                            <td id="td_0230_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0230_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0230_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T0300" class="TimeLine">
                                                0300
                                            </td>
                                            <td id="td_0300_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0300_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0300_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T0330" class="TimeLine">
                                                0330
                                            </td>
                                            <td id="td_0330_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0330_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0330_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T0400" class="TimeLine">
                                                0400
                                            </td>
                                            <td id="td_0400_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0400_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0400_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T0430" class="TimeLine">
                                                0430
                                            </td>
                                            <td id="td_0430_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0430_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0430_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T0500" class="TimeLine">
                                                0500
                                            </td>
                                            <td id="td_0500_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0500_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0500_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T0530" class="TimeLine">
                                                0530
                                            </td>
                                            <td id="td_0530_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0530_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0530_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T0600" class="TimeLine">
                                                0600
                                            </td>
                                            <td id="td_0600_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0600_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0600_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T0630" class="TimeLine">
                                                0630
                                            </td>
                                            <td id="td_0630_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0630_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0630_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T0700" class="TimeLine">
                                                0700
                                            </td>
                                            <td id="td_0700_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0700_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0700_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T0730" class="TimeLine">
                                                0730
                                            </td>
                                            <td id="td_0730_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0730_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0730_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T0800" class="TimeLine">
                                                0800
                                            </td>
                                            <td id="td_0800_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0800_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0800_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T0830" class="TimeLine">
                                                0830
                                            </td>
                                            <td id="td_0830_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0830_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0830_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T0900" class="TimeLine">
                                                0900
                                            </td>
                                            <td id="td_0900_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0900_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0900_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr style="border-bottom: 1px dashed #CCCCCC;">
                                            <td id="T0930" class="TimeLine">
                                                0930
                                            </td>
                                            <td id="td_0930_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0930_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_0930_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T1000" class="TimeLine">
                                                1000
                                            </td>
                                            <td id="td_1000_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1000_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1000_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T1030" class="TimeLine">
                                                1030
                                            </td>
                                            <td id="td_1030_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1030_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1030_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T1100" class="TimeLine">
                                                1100
                                            </td>
                                            <td id="td_1100_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1100_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1100_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T1130" class="TimeLine">
                                                1130
                                            </td>
                                            <td id="td_1130_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1130_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1130_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T1200" class="TimeLine">
                                                1200
                                            </td>
                                            <td id="td_1200_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1200_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1200_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T1230" class="TimeLine">
                                                1230
                                            </td>
                                            <td id="td_1230_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1230_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1230_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T1300" class="TimeLine">
                                                1300
                                            </td>
                                            <td id="td_1300_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1300_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1300_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T1330" class="TimeLine">
                                                1330
                                            </td>
                                            <td id="td_1330_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1330_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1330_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T1400" class="TimeLine">
                                                1400
                                            </td>
                                            <td id="td_1400_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1400_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1400_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T1430" class="TimeLine">
                                                1430
                                            </td>
                                            <td id="td_1430_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1430_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1430_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T1500" class="TimeLine">
                                                1500
                                            </td>
                                            <td id="td_1500_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1500_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1500_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T1530" class="TimeLine">
                                                1530
                                            </td>
                                            <td id="td_1530_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1530_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1530_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T1600" class="TimeLine">
                                                1600
                                            </td>
                                            <td id="td_1600_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1600_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1600_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T1630" class="TimeLine">
                                                1630
                                            </td>
                                            <td id="td_1630_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1630_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1630_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T1700" class="TimeLine">
                                                1700
                                            </td>
                                            <td id="td_1700_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1700_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1700_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T1730" class="TimeLine">
                                                1730
                                            </td>
                                            <td id="td_1730_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1730_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1730_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T1800" class="TimeLine">
                                                1800
                                            </td>
                                            <td id="td_1800_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1800_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1800_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T1830" class="TimeLine">
                                                1830
                                            </td>
                                            <td id="td_1830_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1830_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1830_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T1900" class="TimeLine">
                                                1900
                                            </td>
                                            <td id="td_1900_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1900_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1900_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T1930" class="TimeLine">
                                                1930
                                            </td>
                                            <td id="td_1930_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1930_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_1930_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T2000" class="TimeLine">
                                                2000
                                            </td>
                                            <td id="td_2000_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_2000_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_2000_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T2030" class="TimeLine">
                                                2030
                                            </td>
                                            <td id="td_2030_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_2030_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_2030_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T2100" class="TimeLine">
                                                2100
                                            </td>
                                            <td id="td_2100_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_2100_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_2100_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T2130" class="TimeLine">
                                                2130
                                            </td>
                                            <td id="td_2130_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_2130_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_2130_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T2200" class="TimeLine">
                                                2200
                                            </td>
                                            <td id="td_2200_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_2200_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_2200_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T2230" class="TimeLine">
                                                2230
                                            </td>
                                            <td id="td_2230_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_2230_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_2230_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T2300" class="TimeLine">
                                                2300
                                            </td>
                                            <td id="td_2300_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_2300_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_2300_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="T2330" class="TimeLine">
                                                2330
                                            </td>
                                            <td id="td_2330_R1" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_2330_R2" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                            <td id="td_2330_R3" class="data-cell" onclick="NewBooking(id)" onmouseover="DatacellMouseOver(id)">
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </center>
    <div id="dvBookingPopUp" style="display: none; width: 700px;" title='Booking Details'>
        <iframe id="iFrmBooking" src="" frameborder="0" style="height: 530px; width: 100%">
        </iframe>
    </div>
    <asp:HiddenField ID="hdfisdivCalExists" runat="server" />
    <div id="dvAttendenceStatus" style="display: none; width: 400px; position: absolute;
        background-color: Lime; height: 200px;">
        <table width="100%">
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
