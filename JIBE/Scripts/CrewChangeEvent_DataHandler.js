
function Async_getCrewChangeAlerts(userid) {
    var url = "../webservice.asmx/getCrewChangeAlerts";
    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    var params = "userid=" + userid + "&DateFormat=" + DateFormat.toString();
    obj = new AsyncResponse(url, params, response_getCrewChangeAlerts);
    obj.getResponse();
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
        if(html)
            td.innerHTML = html;

        if (title)
            td.title = title;

        if (cssClass)
            td.className = cssClass;

        tr.appendChild(td);
    }
    catch (ex) {}
    return td;
}

function response_getCrewChangeAlerts(retval) {
    var ar, arS;
    

    if (retval.indexOf('Working') >= 0) { return; }
    try {
        
        retval = clearXMLTags(retval);
        if (retval.indexOf('ERROR:', 0) >= 0 || retval.indexOf('error', 0) >= 0) {
            alert(retval);
            return;
        }

        if (retval.trim().length > 0) {
            document.getElementById('dvCrewChangeAlerts').innerHTML = "";
            var arVal = eval(retval);
            
            var t = document.createElement('table');
            var tb = document.createElement('tbody');
            t.className = "interview-schedule-table tbl-common-Css";

            var th = appendRow(tb, 'interview-schedule-header hdr-common-Css');

            appendCell(th, 'Vessel');
            appendCell(th, 'Event Date');
            appendCell(th, 'Port');
            appendCell(th, 'Crew Name');
            appendCell(th, 'Staff Code');
            appendCell(th, 'Rank');
            appendCell(th, 'Event Type');
            
            tb.appendChild(th);

            for (var i = 0; i < arVal.length; i++) {
                var lstItem = arVal[i];
                var tr = appendRow(tb, "row-common-Css");
                    var sLink = "";

                    appendCell(tr, lstItem.Vessel_Short_Name);
                    appendCell(tr, lstItem.Event_Date);
                    appendCell(tr, lstItem.PORT_NAME);
                    appendCell(tr, lstItem.Staff_FullName);
                    appendCell(tr, "<a href='../Crew/CrewDetails.aspx?ID=" + lstItem.CrewID + "' target='_blank'>" + lstItem.Staff_Code + "</a>");
                    appendCell(tr, lstItem.Rank_Name);
                    appendCell(tr, lstItem.ON_OFF);

            }

            t.appendChild(tb);

            var dvRes = document.getElementById('dvCrewChangeAlerts');
            dvRes.innerHTML = "";
            dvRes.appendChild(t);

            try {
                $("#lblWebPartCrewChangeAlerts").jqxWindow('setContent', document.getElementById('dvCrewChangeAlerts').outerHTML);
            } catch (ex) { }

        } else {

            document.getElementById('dvCrewChangeAlerts').innerHTML = "<span style='color:maroon;padding:2px'> No record found !</span>";
        }
    }
    catch (ex) {  }

}



