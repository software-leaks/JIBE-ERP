$(document).ready(function () {

    RefreshButtonPopups();

});


function RefreshButtonPopups() {
  

    $('.draggable').draggable();

    $('#frmCrewCard').load(function () {
        this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px';
    });

    $("#imgExport").click(function (ev) {
        var pos = $("#imgExport").offset();
        var width = $("#imgExport").width();
        $("#dvExport").css({ "left": (pos.left - 10 * width) + "px", "top": pos.top + "px" });
        $("#dvExport").show();
    });

    $("#dvExport .top").click(function () { $("#dvExport").hide(); });

    $("#dvExport .toggle").click(function () {
        var childid = $(this).attr("child");
        var newSrc = '';

        if ($(this).attr("src").indexOf('down.gif') > -1)
            newSrc = $(this).attr("src").replace("down.gif", "up.gif");
        else
            newSrc = $(this).attr("src").replace("up.gif", "down.gif");

        $(this).attr("src", newSrc);

        $('#' + childid).toggleClass("show");
    });


    $('.interviewSchedule').showBalloon({}).toggle(function () { $(this).hideBalloon(); }, function () { $(this).showBalloon(); });
}


function Timer() {
    try {
        //$('#imgLoading').show();
        var CurrentUserID = $('[id$=hdnUserID]').val();
        if (CurrentUserID != "") {            
            Async_getInterviewsScheduledForToday(CurrentUserID);            
        }

        $('.vesselinfo').InfoBox();

        $('.interviewSchedule').showBalloon({}).toggle(function () { $(this).hideBalloon(); }, function () { $(this).showBalloon(); });

        $('.sailingInfo').SailingInfo();

        //$('.vesselinfo').InfoBox();
        //$('#imgLoading').hide();
        
    }
    catch (e) { $('#imgLoading').hide(); }
}
var mX, mY;

function hideToolTip() {
    $('#dvToolTip').hide();
}
function showRemarks(CardID, evt, obj) {
    try {
        $('#dvToolTip').hide();

        Async_getCrewCardRemarks(CardID,evt,obj);
    }
    catch (e) { }
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

var lastExecutorRemarks = null;
function Async_getCrewCardRemarks(cardid,evt,obj) {
    var CurrentUserID = $('[id$=hdnUserID]').val();
    if (lastExecutorRemarks != null)
        lastExecutorRemarks.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../webservice.asmx', 'getCrewCardRemarks', false, { "cardid": cardid, "userid": CurrentUserID }, response_getCrewCardRemarks, Onfail, new Array(evt, obj));
    lastExecutorRemarks = service.get_executor();
}

function Onfail(){console.log("Error while loading remarks");}

function response_getCrewCardRemarks(retval, prm) {
    var ar, arS;

    if (retval.indexOf('Working...') >= 0) { return; }
    try {
        retval = clearXMLTags(retval).replace(/(?:\r\n|\r|\n)/g, '<br />');
        if (retval.trim().length > 0) {
            var arVal = eval(retval);
            var HTML = "<table width='100%' cellpadding='5' style='border-collapse:collapse;'><tr style='text-align:left;background-color:#cccccc;'><th>Date</th><th>Remarks By</th><th>Remarks</th><tr>";
            for (var i = 0; i < arVal.length; i++) {
                HTML += "<tr><td>" + arVal[i].Date_Of_Creation + "</td><td>" + arVal[i].Created_By + "</td><td>" + arVal[i].Remarks + "</td></tr>";
            }
            HTML += "</table>";

            js_ShowToolTip(HTML, prm[0], prm[1]);
        }
    }
    catch (ex) { console.log(ex.message); }

}

function EditId(querystring) {
    // debugger;
    window.open("CrewDetails.aspx?ID=" + querystring);
}
function openPopupAdd() {
    javascript: window.open("CrewDetails.aspx");
}
function showDiv(dv, id) {
    if (id)
    { document.getElementById("frmCrewCard").src = 'ProposeCrewCard.aspx?CrewID=' + id; }

    document.getElementById(dv).style.display = "block";
    //showModal(dv);
}
function closeDiv(dv) {
    document.getElementById(dv).style.display = "None";
}
function reloadFrame(fr) {
    document.getElementById(fr).src = document.getElementById(fr).src + "&rnd=" + Math.random;
}
