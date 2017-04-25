
function Async_getInterviewResultForCrew(crewid) {
    var url = "../webservice.asmx/getInterviewResultForCrew";
    var params = 'crewid=' + crewid;
    
    obj = new AsyncResponse(url, params, response_getInterviewResultForCrew);
    obj.getResponse();

}

function response_getInterviewResultForCrew(retval) {
    var ar, arS;
    
    if (retval.indexOf('Working...') >= 0) { return; }
    try {
            
        retval = clearXMLTags(retval);
                
        if (retval.trim().length > 0) {


            var arVal = eval(retval);

            
            var t = document.createElement('table');
            var tb = document.createElement('tbody');
            t.className = "interview-result-table";

            var tr = document.createElement('tr');

            for (var i = 0; i < arVal.length; i++) {
                var td = document.createElement('td');
                var dv = document.createElement('div');
                
                dv.innerHTML = GetInterviewerItem(arVal[i]);
                td.appendChild(dv);

                //alert(arVal[i].UserAnswerTable);
                if (arVal[i].UserAnswerTable) {
                    var dvUserTable = document.createElement('div');
                    var sUserTable = GetUserAnswerTable(arVal[i].UserAnswerTable);
                    if (sUserTable != "" && sUserTable != "undefined")
                        dvUserTable.innerHTML = sUserTable;
                    td.appendChild(dvUserTable);
                }
                
                var dvRemarks = document.createElement('div');
                dvRemarks.innerHTML = GetInterviewRemarks(arVal[i]);
                td.appendChild(dvRemarks);


                if (arVal[i].RecomendedVessels) {
                    var dvRecVesselsTable = document.createElement('div');
                    var sRecVesselsTable = GetRecVesselsTable(arVal[i].RecomendedVessels);
                    if (sRecVesselsTable != "" && sRecVesselsTable != "undefined")
                        dvRecVesselsTable.innerHTML = sRecVesselsTable;
                    td.appendChild(dvRecVesselsTable);
                }

                if (arVal[i].RecomendedZones) {
                    var dvRecomendedZones = document.createElement('div');
                    var sRecomendedZones = GetRecZonesTable(arVal[i].RecomendedZones);
                    if (sRecomendedZones != "" && sRecomendedZones != "undefined")
                        dvRecomendedZones.innerHTML = sRecomendedZones;
                    td.appendChild(dvRecomendedZones);
                }
                tr.appendChild(td);
            } //for --
            tb.appendChild(tr);
            t.appendChild(tb);
            

            var dvRes = document.getElementById('dvInterviewResult');
            dvRes.innerHTML = "";
            dvRes.appendChild(t);

            //$("[title]").tooltip();
        }
    }
    catch (ex) { alert(ex.message); }

}
function GetUserAnswerTable(sUserAnswerTable) {
    if (sUserAnswerTable.length > 0) {
        var arVal = eval(sUserAnswerTable);
        var res = "<table class='user-answer-table'>";

        var marks=0;
        var totalMarks = 14;
        var UserRemark = "";

        
        for (var i = 0; i < arVal.length; i++) {
            for (var j = 0; j < arVal[i].length; j++) {

                UserRemark = arVal[i][j].UserRemark.replace("!~","&apos;");

                if(arVal[i][j].SubQuestion != "")
                    res += "<tr><td class='Question'>" + arVal[i][j].Question + "(" + arVal[i][j].SubQuestion + ")" + "</td><td class='ans" + arVal[i][j].UserAnswer + "' title='" + UserRemark + "'>" + arVal[i][j].AnswerDisplay + "</td></tr>";
                else
                    res += "<tr><td class='Question'>" + arVal[i][j].Question + "</td><td class='ans" + arVal[i][j].UserAnswer + "' title='" + UserRemark + "'>" + arVal[i][j].AnswerDisplay + "</td></tr>";

                if (arVal[i][j].UserAnswer == "0") totalMarks -= 1;
                if(arVal[i][j].UserAnswer == "1") marks+= 5;
                if(arVal[i][j].UserAnswer == "2") marks+= 3;
                if(arVal[i][j].UserAnswer == "3") marks+= 2;
                if (arVal[i][j].UserAnswer == "4") marks += 0;

            }

        }        
        var d = parseFloat(marks) / totalMarks;
        var avg = Math.round(d * 100) / 100;


        res += "<tr><td style='background-color:yellow;font-weight:bold;'>Average Mark&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(Out of 5) :</td><td style='background-color:yellow;font-weight:bold;text-align:center'>" + avg + "</td></tr>";
        res += "</table>";

        return res;
    }
}

function GetRecVesselsTable(sRecVesselsTable) {
    if (sRecVesselsTable.length > 0) {
        var arVal = eval(sRecVesselsTable);
        var res = "<table class='user-answer-table'  style='border:1px solid outset;background-color:#efefef'><tr><td>Recomended for vessel:</td></tr><tr><td><table >";

        for (var i = 0; i < arVal.length; i++) {
            for (var j = 0; j < arVal[i].length; j++) {

                if (arVal[i][j].VesselID != "")
                    res += "<tr><td><img src='../images/checked.gif'></td><td class='Question'>" + arVal[i][j].Vessel_Name + "</td></tr>";
            }
        }

        res += "</table></td></tr></table>";

        return res;
    }
}
function GetRecZonesTable(sRecZonesTable) {
    if (sRecZonesTable.length > 0) {
        var arVal = eval(sRecZonesTable);
        var res = "<table class='user-answer-table'  style='border:1px solid outset;background-color:#efefef'><tr><td>Recomended for Tradeing Zones:</td></tr><tr><td><table >";

        for (var i = 0; i < arVal.length; i++) {
            for (var j = 0; j < arVal[i].length; j++) {

                if (arVal[i][j].VesselID != "")
                    res += "<tr><td><img src='../images/checked.gif'></td><td class='Question'>" + arVal[i][j].Zone + "</td></tr>";
            }
        }

        res += "</table></td></tr></table>";

        return res;
    }
}


function GetInterviewerItem(interviewItem) {
    var Id = interviewItem.ID;
    
    var IntvDt = new Date(interviewItem.InterviewDate);
    var IntvPlanDt = new Date(interviewItem.InterviewPlanDate);

    var CrewID = interviewItem.CrewID;
    var Candidate = interviewItem.CandidateName;
    var Interviewer = interviewItem.Planned_Interviewer;
    var InterviewerID = interviewItem.PlannedInterviewerID;
    var Office_Dept = interviewItem.Office_Dept;
    var Result = interviewItem.Result;

    if (Result == '1') {
        Result = "<td colspan=2 class='interview-title-approved'>APPROVED</td>";
    }
    else if (Result == '0') {
        Result = "<td colspan=2 class='interview-title-rejected'>REJECTED</td>";
    }
    else {
        Result = "<td colspan=2 class='interview-title-pending'>PENDING</td>";
    }
    //var res = "<table class='interviewer-table' onmouseover='onMouseOver(this)' onmouseout='onMouseOut(this)' onclick='InterviewonClick(" + CrewID + "," + Id + ")'>";

    var res = "<div><table class='interviewer-table'>";
    res += "<tr><a href='interview.aspx?id=" + Id + "' target=_blank>" + Result + "</a></tr>";
    res += "<tr><td>Interviewer</td><td>" + Interviewer + "</td></tr>";
    res += "<tr><td>Deptartment</td><td>" + Office_Dept + "</td></tr>";

    if (interviewItem.InterviewDate) {
        res += "<tr><td>Date</td><td>" + IntvDt.toDateString() + "</td></tr>";
        res += "<tr><td>Time</td><td><font color=blue>" + IntvDt.toLocaleTimeString() + "</font></td></tr>";
    }
    else {
        res += "<tr><td>Plan Date</td><td>" + IntvPlanDt.toDateString() + "</td></tr>";
        res += "<tr><td>Time</td><td><font color=blue>" + IntvPlanDt.toLocaleTimeString() + "</font></td></tr>";
    }
    res += "</table></div>";

    return res;
}

function GetInterviewResult() {
    var CrewID = $('[id$=HiddenField_CrewID]').val();
    Async_getInterviewResultForCrew(CrewID);    
}

function GetInterviewRemarks(interviewItem)
{
    var res = "<table class='interview-remarks-table'><tr><td><b>Remarks:</b></td></tr>";
    res += "<tr><td>" + interviewItem.ResultText + "</td></tr>";
    res += "</table>"
    return res;
}

function createDvElement(id, text, selected, title) {
    try {
        var oDiv = document.createElement('div');
        oDiv.id = 'dv' + id;
        oDiv.name = 'dvCandidate';

        // set attributes
        //oDiv.style.position = "absolute";
        if (selected == 1) {
            oDiv.style.backgroundColor = 'Yellow';
            oDiv.style.color = '#000000';
            oDiv.style.fontWeight = 'bold';
        }
        else {
            oDiv.style.backgroundColor = '#5858FA';
            oDiv.style.color = '#ffffff';
        }
        oDiv.style.padding = "0px";
        //oDiv.style.overflow = "hidden";
        //oDiv.style.zIndex = "-1";

        oDiv.style.marginLeft = "1px";
        oDiv.style.marginRight = "0px";
        oDiv.style.marginTop = "2px";  //((iTop * 12) - 12) + "px";

        oDiv.style.width = "280px";
        //oDiv.style.height = "12px";

        oDiv.style.opacity = "0.1";
        oDiv.style.textAlign = "center";

        oDiv.style.visibility = "visible";
        oDiv.style.cursor = 'hand';
        oDiv.title = title;

        oDiv.innerHTML = text;


    }
    catch (ex) { alert(ex.message); }
    return oDiv;
}

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





