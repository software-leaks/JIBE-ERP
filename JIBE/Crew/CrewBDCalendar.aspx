<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewBDCalendar.aspx.cs" Title="Crew Birthday Calendar"
    Inherits="Crew_CrewBDCalendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- full calander css -->
    <link rel="stylesheet" href="../css/bootstrap.min.css?v=4" />
    <link rel="stylesheet" href="../css/bootstrap-responsive.min.css?v=1" />
    <link rel="stylesheet" href="../css/fullcalendar.css?v=1" />
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/smscss_blue.css" />
    <!-- REQUIRED: jS -->
    <script type="text/javascript" src="../js/libs/jquery.min.js"></script>
    <script type="text/javascript" src="../js/libs/jquery-ui.min.js"></script>
    <script src="../scripts/jquery.fullcalendar.min.js" type="text/javascript"></script>
    <!-- REQUIRED: Datatable components -->
    <script type="text/javascript" src="../js/include/selectnav.min.js"></script>
    <script type="text/javascript" src="../js/include/jquery.uniform.min.js"></script>
    <script type="text/javascript" src="../js/include/jarvis.widget.min.js"></script>
    <script type="text/javascript" src="../Scripts/CrewIcon.js"></script>
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
                        url: '../UserControl/Crew_BirthDayCalendar.ashx',
                    }],
                    viewDisplay : function(view,element) {
                        //alert();
                        //var d = $("#calendar").fullCalendar('getDate');
                        //alert("The current date of the calendar is " + d);
                        //
                    },
                    loading: function(bool) {
				        if (bool) $('#dvProgress').show();
				        else {$('#dvProgress').hide(); //$(".crewIcon").CrewIcon();
                        }
			        }
                    ,eventRender: function(event, eventElement) {                        
                      if (event.icon) {
                          if (eventElement.find('.fc-event-title').length) {
                            eventElement.find('.fc-event-title').before($("<span class=\"fc-event-icon\"></span>").html("<img class=\"crewIcon\" src=\"../Uploads/CrewImages/" + event.icon + "\" />"));
                          } 
                      }
                    }
                });
            };

           
        }

        /* end calendar */

        function viewCrewDetails(CrewID) {
            window.open("CrewDetails.aspx?id=" + CrewID);
        }
    </script>
    <style type="text/css">
        .fc .fc-event-icon .crewIcon
        {
            height:40px;
            -webkit-border-radius: 100%;
            -moz-border-radius: 100%;
            border-radius: 100%;
            border: 2px solid #fff;
            margin:2px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <!-- new widget -->
    <div style="margin: 10px; padding: 10px;">
        <div class="page-title">
            Birthday Calendar
        </div>
        <div id="dvPageContent" class="page-content-main">
            <div style="text-align: center; height: 20px;">
                <img id="dvProgress" src="../Images/loaderbar.gif" alt="Please Wait" />
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
