var DateDiff = {

    inDays: function (d1, d2) {
        var t2 = d2.getTime();
        var t1 = d1.getTime();

        return parseInt((t2 - t1) / (24 * 3600 * 1000));
    },

    inWeeks: function (d1, d2) {
        var t2 = d2.getTime();
        var t1 = d1.getTime();

        return parseInt((t2 - t1) / (24 * 3600 * 1000 * 7));
    },

    inMonths: function (d1, d2) {
        var d1Y = d1.getFullYear();
        var d2Y = d2.getFullYear();
        var d1M = d1.getMonth();
        var d2M = d2.getMonth();

        return (d2M + 12 * d2Y) - (d1M + 12 * d1Y);
    },

    inYears: function (d1, d2) {
        return d2.getFullYear() - d1.getFullYear();
    }
}

function Async_getDocumentExpiryList(userid) {

    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    var url = "../webservice.asmx/getDocumentExpiryList";
    var params = 'user_id=' + userid + '&DateFormat=' + DateFormat.toString();
    obj = new AsyncResponse(url, params, response_getDocumentExpiryList);
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

function response_getDocumentExpiryList(retval) {
    var ar, arS;

    if (retval.indexOf('Working') >= 0) { return; }
    try {

        retval = clearXMLTags(retval);
        if (retval.indexOf('ERROR:', 0) >= 0 || retval.indexOf('error:', 0) >= 0) {
            alert(retval);
            return;
        }

        if (retval.trim().length > 0) {
            document.getElementById('dvDocExpiryAlerts').innerHTML = "";
            var arVal = eval(retval);
            
            var t = document.createElement('table');
            var tb = document.createElement('tbody');
            t.className = "interview-schedule-table tbl-common-Css";

            var th = appendRow(tb, 'fixed-header hdr-common-Css');

            appendCell(th, 'Vessel');
            appendCell(th, 'S.Code');
            appendCell(th, 'Staff Name');
            appendCell(th, 'Rank');
            appendCell(th, 'Doc Type');
            appendCell(th, 'Document Name');
            appendCell(th, 'Expiry Date');
            appendCell(th, 'Days Left');
            
            tb.appendChild(th);

            for (var i = 0; i < arVal.length; i++) {
                var lstItem = arVal[i];
                var tr = appendRow(tb, "row-common-Css");
                    var sLink = "";

                    appendCell(tr, lstItem.Vessel_Short_Name);
                    appendCell(tr, "<a href='../Crew/CrewDetails.aspx?ID=" + lstItem.CrewID + "' target='_blank'>" + lstItem.Staff_Code + "</a>");
                    appendCell(tr, lstItem.CrewName);
                    appendCell(tr, lstItem.Rank_Short_Name);
                    appendCell(tr, lstItem.DocTypeName);
                    
                    var docName = (lstItem.DocName.length>20)?(lstItem.DocName.substr(0, 20) + "..."):lstItem.DocName;

                    sLink = "<a href='../DMS/Default.aspx?ID=" + lstItem.CrewID + "&DocID=" + lstItem.DocID + "' target='_blank' title='" + lstItem.DocName + "'>" + docName + "</a>";
                    appendCell(tr, sLink);
                    
                    var now = new Date();

                    
//                    if (DateDiff.inDays(now, lstItem.DateOfExpiry) < 0)
//                        appendCell(tr, myDateFormat(lstItem.DateOfExpiry), 'Days Left:' + lstItem.DaysLeft, 'doc-overdue');
//                    else if (DateDiff.inDays(now, lstItem.DateOfExpiry) > 0 && DateDiff.inDays(now, lstItem.DateOfExpiry) < 14)
//                        appendCell(tr, myDateFormat(lstItem.DateOfExpiry), 'Days Left:' + lstItem.DaysLeft, 'doc-due');
//                    else
//                        appendCell(tr, myDateFormat(lstItem.DateOfExpiry), 'Days Left:' + lstItem.DaysLeft);

                    appendCell(tr, myDateFormat(lstItem.DateOfExpiry), 'Days Left:' + lstItem.DaysLeft, lstItem.Overdue);

                    if (lstItem.DaysLeft > 0)
                        appendCell(tr, lstItem.DaysLeft + ' Days');
                    else
                        appendCell(tr, '<font color=red>Expired!</font>');

            }

            t.appendChild(tb);

            var dvRes = document.getElementById('dvDocExpiryAlerts');
            dvRes.innerHTML = "";
            dvRes.appendChild(t);

            try {
                $("#lblWebPartDocumentsexpire").jqxWindow('setContent', document.getElementById('dvDocExpiryAlerts').outerHTML);
            } catch (ex) { }

        } else {

            document.getElementById('dvDocExpiryAlerts').innerHTML = "<span style='color:maroon;padding:2px'> No record found !</span>";
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
