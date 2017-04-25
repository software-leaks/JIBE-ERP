
function Async_getAllComplaintList(userid) {
    var url = "../webservice.asmx/getAllComplaintList";
    var params = 'user_id=' + userid;
    //alert(params);
    obj = new AsyncResponse(url, params, response_getAllComplaintList);
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

function response_getAllComplaintList(retval) {
    var ar, arS;

    if (retval.indexOf('Working') >= 0) { return; }
    try {

        retval = clearXMLTags(retval);
        if (retval.indexOf('ERROR:', 0) >= 0 || retval.indexOf('error', 0) >= 0) {
            alert(retval);
            return;
        }

        if (retval.trim().length > 0) {
            document.getElementById('dvDocExpiryAlerts').innerHTML = "";
            var arVal = eval(retval);
            
            var t = document.createElement('table');
            var tb = document.createElement('tbody');
            t.className = "interview-schedule-table";

            var th = appendRow(tb, 'crew-complaint-header');

            appendCell(th, 'Crew Name');
            appendCell(th, 'Staff Code');
            appendCell(th, 'Vessel');
            appendCell(th, 'Doc Type');
            appendCell(th, 'Document Name');
            appendCell(th, 'Expiry Date');
            appendCell(th, 'Days Left');
            
            tb.appendChild(th);

            for (var i = 0; i < arVal.length; i++) {
                var lstItem = arVal[i];
                    var tr = appendRow(tb);
                    var sLink = "";

                    appendCell(tr, lstItem.CrewName);
                    appendCell(tr, "<a href='../Crew/CrewDetails.aspx?ID=" + lstItem.CrewID + "' target='_blank'>" + lstItem.Staff_Code + "</a>");
                    appendCell(tr, lstItem.Vessel_Short_Name);
                    appendCell(tr, lstItem.DocTypeName);

                    sLink = "<a href='../DMS/Default.aspx?ID=" + lstItem.CrewID + "&DocID=" + lstItem.DocID + "' target='_blank'>" + lstItem.DocName + "</a>";
                    appendCell(tr, sLink);

                    appendCell(tr, myDateFormat(lstItem.DateOfExpiry), 'Days Left:' + lstItem.DaysLeft, 'redbox');
                    
                    if (lstItem.DaysLeft > 0)
                        appendCell(tr, lstItem.DaysLeft);
                    else
                        appendCell(tr, '<font color=red>Expired!</font>');

            }

            t.appendChild(tb);

            var dvRes = document.getElementById('dvDocExpiryAlerts');
            dvRes.innerHTML = "";
            dvRes.appendChild(t);

        }
    }
    catch (ex) {  }

}


//--------------------------------------------------------------------------------------------------------------

function myDateFormat(sDt) {
    //var dt = new Date(sDt);
    //return dt.getDate() + '/' + (parseInt(dt.getMonth())+1).toString() + '/' + dt.getFullYear();

    return sDt;
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
