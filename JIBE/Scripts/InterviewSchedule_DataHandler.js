
function Async_getInterviewsScheduledForToday(userid) {
    var url = "../webservice.asmx/getInterviewsScheduledForToday";
    var params = 'userid=' + userid;
    obj = new AsyncResponse(url, params, response_getInterviewsScheduledForToday);
    obj.getResponse();
}

function response_getInterviewsScheduledForToday(retval) {
    var ar, arS;
    if (retval.indexOf('Working') >= 0) { return; }
    try {

        retval = clearXMLTags(retval);
        if (retval.indexOf('ERROR:', 0) >= 0 || retval.indexOf('error', 0) >= 0) {
            return;
        }

        if (retval.trim().length > 0) {
            var arVal = eval(retval);

            var t = document.createElement('table');
            var tb = document.createElement('tbody');
            t.className = "interview-schedule-table";
            //alert(retval);
            var tr = document.createElement('tr');
            var j = 0;

            for (var i = 0; i < arVal.length; i++) {
                var interviewItem = arVal[i];
                var ID = interviewItem.ID;
                var IQID = interviewItem.IQID;
                var IntvPlanDt = new Date(interviewItem.InterviewPlanDate);
                var Candidate = interviewItem.CandidateName;
                var Interviewer = interviewItem.Interviewer;                
                var Office_Dept = interviewItem.Office_Dept;
                var CrewID = interviewItem.CrewID;
                var Staff_Code = interviewItem.Staff_Code;
                var PlanDays = interviewItem.PlanDays;

                var title = "Staff Code: " + Staff_Code + "<br>Candidate: " + Candidate + "<br>Interviewer: " + Interviewer + "<br>Date: " + IntvPlanDt.toDateString() + "<br>Time: <font color=yellow>" + IntvPlanDt.toLocaleTimeString() + "</font>";

                if (PlanDays == "0") {
                    
                    var td = document.createElement('td');
                    var dv = createDvElement(i, '&nbsp;', title, 'interviewScheduleIcon');

                    if (IQID > 0)
                        dv.innerHTML = "<a href='CrewInterview.aspx?ID=" + ID + "&CrewID=" + CrewID + "' target='_blank'><img src='../Images/Interview_1.png' border=0></a>"
                    else
                        dv.innerHTML = "<a href='Interview.aspx?ID=" + ID + "&CrewID=" + CrewID + "' target='_blank'><img src='../Images/Interview_1.png' border=0></a>"
                    
                    td.appendChild(dv);
                    tr.appendChild(td);
                    j++;
                }
            }

            var td1 = document.createElement('td');
            var dv1 = createDvElement(i, '&nbsp;&nbsp;&nbsp;&nbsp;Today:&nbsp;(' + j +')&nbsp;&nbsp;All:&nbsp;(' + (i+1) + ')', '', '');
            td1.appendChild(dv1);
            tr.appendChild(td1);
            
            tb.appendChild(tr);
            t.appendChild(tb);
            
            var dvRes = document.getElementById('dvInterviewSchedule');
            dvRes.innerHTML = "";
            dvRes.appendChild(t);

            $('.interviewScheduleIcon').tooltip();            
        }
    }
    catch (ex) { 
        alert(ex.message); 
    }
}

function createDvElement(id_, text_,  title_, class_) {
    try {
        var oDiv = document.createElement('div');
        oDiv.id = 'dv' + id_;
        oDiv.name = 'dv' + id_;
        oDiv.title = title_;
        oDiv.innerHTML = text_;
        oDiv.className = class_;
    }
    catch (ex) { alert(ex.message); }
    return oDiv;
}

//--------------------------------------------------------------------------------------------------------------

function Async_getPendingInterviewList(userid) {
    var url = "../webservice.asmx/getPendingInterviewList";
    var params = 'userid=' + userid;

    obj = new AsyncResponse(url, params, response_getPendingInterviewList);
    obj.getResponse();
}
function response_getPendingInterviewList(retval) {
    var ar, arS;

    if (retval.indexOf('Working') >= 0) { return; }
    try {

        retval = clearXMLTags(retval);
        if (retval.indexOf('ERROR:', 0) >= 0 || retval.indexOf('error', 0) >= 0) {
            alert(retval);
            return;
        }

        if (retval.trim().length > 0) {
            document.getElementById('dvInterviewSchedules').innerHTML = "";
            var arVal = eval(retval);

            var t = document.createElement('table');
            var tb = document.createElement('tbody');
            t.className = "interview-schedule-table";


            var th = document.createElement('tr');
            th.className = "interview-schedule-header";
                                    
            var tdsc = document.createElement('td');
            var tdc = document.createElement('td');
            var tdi = document.createElement('td');
            var tdd = document.createElement('td');

            tdsc.innerHTML = 'Staff Code';
            tdc.innerHTML = 'Candidate Name';
            tdi.innerHTML = 'Interviewer';
            tdd.innerHTML = 'Planned Date';


            th.appendChild(tdsc);
            th.appendChild(tdc);
            th.appendChild(tdi);
            th.appendChild(tdd);

            tb.appendChild(th);

            for (var i = 0; i < arVal.length; i++) {                
                var interviewItem = arVal[i];

                var ID = interviewItem.ID;
                var Staff_Code = interviewItem.Staff_Code;
                var CrewID = interviewItem.CrewID;
                var IntvPlanDt = interviewItem.InterviewPlanDate_Time;
                var Candidate = interviewItem.CandidateName;
                var Interviewer = interviewItem.Interviewer;
                var Office_Dept = interviewItem.Office_Dept;

                var tr = document.createElement('tr');                

                var td1 = document.createElement('td');
                var dv1 = createDvElement(i, Staff_Code, '', '_link');
                dv1.setAttribute('onclick', "javascript:window.open('../crew/CrewDetails.aspx?ID=" + CrewID + "');");
                td1.appendChild(dv1);

                var td2 = document.createElement('td');
                var dv2 = createDvElement(i + 100, Candidate, '', '_link');
                dv2.setAttribute('onclick', "javascript:window.open('../crew/CrewDetails.aspx?ID=" + CrewID + "');");
                td2.appendChild(dv2);

                var td3 = document.createElement('td');
                td3.innerHTML = Interviewer;

                var td4 = document.createElement('td');
                td4.innerHTML = IntvPlanDt;

                tr.appendChild(td1);
                tr.appendChild(td2);
                tr.appendChild(td3);
                tr.appendChild(td4);

                tb.appendChild(tr);
            }
            
            t.appendChild(tb);

            var dvRes = document.getElementById('dvInterviewSchedules');
            dvRes.innerHTML = "";
            dvRes.appendChild(t);

            //$("#dvInterviewSchedules [title]").tooltip();

        }
    }
    catch (ex) {alert(ex.Message); }

}

//------------------------------------//
//-------------by user----------------// 

function Async_getPendingInterviewList_By_UserID(userid) {
    var url = "../webservice.asmx/getPendingInterviewList_By_UserID";
    var params = 'userid=' + userid;

    obj = new AsyncResponse(url, params, response_getPendingInterviewList_By_UserID);
    obj.getResponse();
}
function response_getPendingInterviewList_By_UserID(retval) {
    var ar, arS;

    if (retval.indexOf('Working') >= 0) { return; }
    try {

        retval = clearXMLTags(retval);
        if (retval.indexOf('ERROR:', 0) >= 0 || retval.indexOf('error', 0) >= 0) {
            alert(retval);
            return;
        }

        if (retval.trim().length > 0) {
            document.getElementById('dvInterviewSchedules_By_UserID').innerHTML = "";
            var arVal = eval(retval);

            var t = document.createElement('table');
            var tb = document.createElement('tbody');
            t.className = "interview-schedule-table";
            //alert(retval);            

            var th = document.createElement('tr');
            th.className = "Candidate";
            var tdc = document.createElement('td');
            var tdi = document.createElement('td');
            var tdd = document.createElement('td');
            var tdt = document.createElement('td');

            tdc.innerHTML = 'Candidate Name';
            tdi.innerHTML = 'Interviewer';
            tdd.innerHTML = 'Planned Date';
//            tdt.innerHTML = 'Time';

            th.appendChild(tdc);
            th.appendChild(tdi);
            th.appendChild(tdd);
//            th.appendChild(tdt);
            tb.appendChild(th);

            for (var i = 0; i < arVal.length; i++) {
                var interviewItem = arVal[i];

                var ID = interviewItem.ID;
                var CrewID=interviewItem.CrewID;
                var IntvPlanDt = interviewItem.InterviewPlanDate_Time;
                var Candidate = interviewItem.CandidateName;
                var Interviewer = interviewItem.Interviewer;
                var Office_Dept = interviewItem.Office_Dept;

                var tr = document.createElement('tr');
                //var title = "Candidate: " + Candidate + "<br>Interviewer: " + Interviewer + "<br>Date: " + IntvPlanDt.toDateString() + "<br>Time: <font color=yellow>" + IntvPlanDt.toLocaleTimeString() + "</font>";

                var td = document.createElement('td');
                var dv = createDvElement(i, IntvPlanDt, '','');
                //dv.className = "PlanDate";
                td.appendChild(dv);

                var td2 = document.createElement('td');
                var dv2 = createDvElement(i + 100, Candidate, '','');
                //dv2.className = "Candidate";
                td2.appendChild(dv2);

                
                var td3 = document.createElement('td');
                td3.innerHTML = Interviewer;

//                var td4 = document.createElement('td');
//                td4.innerHTML = IntvPlanDt.toLocaleTimeString();

                tr.appendChild(td2);
                tr.appendChild(td3);
                tr.appendChild(td);
//                tr.appendChild(td4);
                tr.setAttribute('onclick', "javascript:window.open('../crew/Interview.aspx?ID=" + ID + "&CrewID=" + CrewID + "');");
              

                tb.appendChild(tr);
            }

            t.appendChild(tb);

            var dvRes = document.getElementById('dvInterviewSchedules_By_UserID');
            dvRes.innerHTML = "";
            dvRes.appendChild(t);

            //$("#dvInterviewSchedules [title]").tooltip();

        }
    }
    catch (ex) { }

}

//-------------by user-------------

//function clearXMLTags(retval) {
//    try {
//        retval = retval.replace('<?xml version="1.0" encoding="utf-8"?>', '');
//        retval = retval.replace('<string xmlns="http://tempuri.org/" />', '');
//        retval = retval.replace('<string xmlns="http://tempuri.org/">', '');
//        retval = retval.replace('</string>', '');
//    }
//    catch (ex) { alert(ex.message); }
//    return retval;
//}





