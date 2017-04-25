String.prototype.trim = function () {
    return this.replace(/^\s*/, "").replace(/\s*$/, "");
}
var dateformat1 = "";
function Async_getPlannedInterviewForTheMonth(userid, crewid, m, y, showcalforall, dateformat) {

    dateformat1 = dateformat;
    var url = "../webservice.asmx/getPlannedInterviewForTheMonth";
    var params = 'userid=' + userid + '&crewid=' + crewid + '&m=' + m + '&y=' + y + '&showcalforall=' + showcalforall;
    //alert(params);@ShowCalForAll
    obj = new AsyncResponse(url, params, response_getPlannedInterviewForTheMonth);
    obj.getResponse();

}
function zeroPad(number, len) {
    // Left-pad the given number with zeros into a string of the given length.
    //
    //  Notes:  - Input must be >= 0.
    //          - Length is to the left of the deciml, so zeroPad(2.17, 3) => 002.17

    var padding = '00000000000000000000000000000000000000000000000000000000000000000000';

    if (number <= 0)
        return padding.substring(0, len);
    else
        return padding.substring(0, len - (Math.floor(Math.log(number) / Math.log(10)) + 1)) + String(number);
};
// Function will display Interviewer Name,Department ,Interview date  & Interview Time on tooltip




function response_getPlannedInterviewForTheMonth(retval) {
    var ar, arS;
    if (retval.indexOf('Working') >= 0) { return; }
    try {
        retval = clearXMLTags(retval);
        if (retval.indexOf('ERROR:', 0) >= 0 || retval.indexOf('error', 0) >= 0) {
            alert(retval);
            return;
        }

        if (retval.trim().length > 0) {
            var arVal = eval(retval);

            for (var i = 0; i < arVal.length; i++) {
                var Id = arVal[i].ID;
                var IntvPlanDt = new Date(arVal[i].InterviewPlanDate1);
                if (IntvPlanDt == "Invalid Date") {///This is for IE, beacuase IE cannot directly convert Date eq.(new Date("2017-01-02 11:52:25")) so we have used(new Date(year,month,date,hour,minute,seconds))
                    var dateTime = arVal[i].InterviewPlanDate1.split(' ');
                    if (dateTime[0] != "") {
                        var dt = dateTime[0].split("-");
                        var tm = dateTime[1].split(":");

                        IntvPlanDt = new Date(dt[0], parseInt(dt[1]) - 1, dt[2], tm[0], tm[1], tm[2]);
                    }
                }
                var FormatedDate = arVal[i].InterviewPlanDateFormat;
                var result = '';
                var hour = IntvPlanDt.getHours();
                var x;
                if (hour >= 12) {
                    x = 'PM';
                    hour -= 12;
                }
                else
                    x = 'AM';

                result = zeroPad(hour, 2) + ':' +
                                    zeroPad(IntvPlanDt.getMinutes(), 2) + ':' +
                                    zeroPad(IntvPlanDt.getSeconds(), 2) + ' ' + x;

                var CrewID = arVal[i].CrewID;
                var Candidate = arVal[i].CandidateName;
                var Interviewer = arVal[i].Interviewer;
                var InterviewerID = arVal[i].PlannedInterviewerID;
                var Selected = arVal[i].Selected;
                var Office_Dept = arVal[i].Office_Dept;
                var MyDept = arVal[i].MyDept;
//                if (dateformat1 == '')
//                    dateformat1 = 'dd-mm-yyyy';
                var strDate = formatDateToString(IntvPlanDt, dateformat1);
                var title = "<table style='font-size:11px;'><tr><td><b>Interviewer</b></td><td>:</td><td>" + Interviewer + "</td></tr><tr><td><b>Deptartment</b></td><td>:</td><td>" + Office_Dept + "</td></tr><tr><td><b>Date</b></td><td>:</td><td>" + FormatedDate + "</td></tr><tr><td><b>Time</b></td><td>:</td><td>" + result + "</td></tr></table>";
                
                var dv = createDvElement(Id, Candidate, Selected, title);

                if (MyDept == "0" && Selected == "0")
                    dv.style.backgroundColor = 'Gray';

                var tdId = IntvPlanDt.getMonth() + '_' + IntvPlanDt.getDate();

                var obj = document.getElementById(tdId);
                if (obj)
                    obj.appendChild(dv);

            }
        }
    }
    catch (ex) { alert(ex.message); }
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
        oDiv.style.overflow = "hidden";
        oDiv.style.zIndex = "-1";

        oDiv.style.marginLeft = "1px";
        oDiv.style.marginRight = "0px";
        oDiv.style.marginTop = "2px";  //((iTop * 12) - 12) + "px";

        oDiv.style.width = "80px";
        oDiv.style.height = "12px";

        //oDiv.style.opacity = "0.1";
        oDiv.style.textAlign = "center";

        oDiv.style.visibility = "visible";
        oDiv.style.cursor = 'hand';
        //oDiv.title=title;
        var att = document.createAttribute("rel");
        att.value = title;
        oDiv.setAttributeNode(att);

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

function genHex() {
    colors = new Array(14)
    colors[0] = "0"
    colors[1] = "1"
    colors[2] = "2"
    colors[3] = "3"
    colors[4] = "4"
    colors[5] = "5"
    colors[5] = "6"
    colors[6] = "7"
    colors[7] = "8"
    colors[8] = "9"
    colors[9] = "a"
    colors[10] = "b"
    colors[11] = "c"
    colors[12] = "d"
    colors[13] = "e"
    colors[14] = "f"

    digit = new Array(5)
    color = ""
    for (i = 0; i < 6; i++) {
        digit[i] = colors[Math.round(Math.random() * 14)]
        color = color + digit[i]
    }

    return "#" + color;
}
