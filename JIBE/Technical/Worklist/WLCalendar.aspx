<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WLCalendar.aspx.cs" Title="Worklist Calendar" Inherits="Technical_Worklist_WLCalendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- full calander css -->
    <link rel="stylesheet" href="../../css/bootstrap.min.css?v=4" />
    <link rel="stylesheet" href="../../css/bootstrap-responsive.min.css?v=1" />
    <link rel="stylesheet" href="../../css/fullcalendar.css?v=1" />
    <link rel="stylesheet" href="../../Styles/jquery-ui.css"/>
    <link rel="stylesheet" href="../../Styles/smscss_blue.css" />
    
    <!-- REQUIRED: jS -->
    <script type="text/javascript" src="../../js/libs/jquery.min.js"></script>
    <script type="text/javascript" src="../../js/libs/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery.fullcalendar.min.js" ></script>

    <!-- REQUIRED: Datatable components -->
    <script type="text/javascript" src="../../js/include/selectnav.min.js"></script>
    <script type="text/javascript" src="../../js/include/jquery.uniform.min.js"></script>
    <script type="text/javascript" src="../../js/include/jarvis.widget.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            /* draw calendar */
            setup_calendar();
        });

        /* ---------------------------------------------------------------------- */
        /*	Calendar
        /* ---------------------------------------------------------------------- */

        function setup_calendar() {

            if ($("#calendar").length) {
                var date = new Date();
                var d = date.getDate();
                var m = date.getMonth();
                var y = date.getFullYear();

                var calendar = $('#calendar').fullCalendar({
                    header: {
                        left: 'title', //,today
                        center: 'prev, next, today',
                        right: 'month, agendaWeek, agenDay' //month, agendaDay, 
                    },
                    selectable: true,
                    selectHelper: false,
                    editable: false,
                    eventSources: [{
                        url: '../../UserControl/Worklist_fullCalendar.ashx?J=WL&Dt=' + $('#calendar').fullCalendar('getDate').toString(),
                    }, {
                        url: '../../UserControl/Worklist_fullCalendar.ashx?J=PMS&Dt=' + $("#calendar").fullCalendar('getDate').toString(),
                        color: 'grey', // an option!
                        textColor: 'black' // an option!
                    }],
                    viewDisplay : function(view,element) {
                        //alert();
                        //var d = $("#calendar").fullCalendar('getDate');
                        //alert("The current date of the calendar is " + d);
                    },
                    loading: function(bool) {
				        if (bool) $('#dvProgress').show();
				        else $('#dvProgress').hide();
			        }
                });
            };

            // events: "../../UserControl/Worklist_fullCalendar.ashx"
            /* hide default buttons */
            //$('.fc-header-right, .fc-header-center').hide();

        }

        /* end calendar */

        function viewWorklistDetails(vid, wlid, offid) {
            window.open("ViewJob.aspx?OFFID=" + offid + "&WLID=" + wlid + "&VID=" + vid);
        }
        function viewPMSJobDetails(vid, jid, jhid) {
            window.open("../PMS/PMSJobIndividualDetails.aspx?JobID="+ jid +"&JobHistoryID="+ jhid +"&VID="+ vid +"&Qflag=H");
        }
    </script>
    <style type="text/css">
        .PMS-Pending div span.fc-event-title
        {
            color: Yellow;
        }
        .PMS-Done-ExcessTime div span.fc-event-title
        {
            color: Red;
        }
        .PMS-Done-InTime div span.fc-event-title
        {
        }
        
        .WL-Pending div span.fc-event-title
        {
            color: Yellow;
        }
        .WL-Done-ExcessTime div span.fc-event-title
        {
            color: Red;
        }
        .WL-Done-InTime div span.fc-event-title
        {
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <!-- new widget -->
    <div style="margin: 10px; padding: 10px;">
        
        <div class="page-title">
            Worklist Calendar
        </div>
        <div id="dvPageContent" class="page-content-main">
            <div style="text-align:center;height:20px;">
                <img id="dvProgress" src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>

            <div>
                <!-- end widget edit box -->
                <div class="inner-spacer">
                    <!-- content goes here -->
                    <!-- calnedar container -->
                    <div id="calendar-container">
                        <div class="dt-header calender-spacer">
                        </div>
                        <div id="calendar">
                        </div>
                    </div>
                    <!-- end calendar container -->
                </div>
            </div>
            <!-- end widget div -->
        </div>
    </div>
    
    </form>
</body>
</html>
