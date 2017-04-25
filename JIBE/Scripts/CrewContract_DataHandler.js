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

function Async_getContract_ToSign_Alerts(userid) {
    var url = "../webservice.asmx/getContracts_ForDigiSign_Alerts";
    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    var params = 'userid=' + userid + '&DateFormat=' + DateFormat.toString();
    obj = new AsyncResponse(url, params, response_getContracts_ForDigiSign_Alerts);
    obj.getResponse();
}

function response_getContracts_ForDigiSign_Alerts(retval) {
    var ar, arS;

    if (retval.indexOf('Working') >= 0) { return; }
    try {

        retval = clearXMLTags(retval);
        if (retval.indexOf('ERROR:', 0) >= 0 || retval.indexOf('error', 0) >= 0) {
            alert(retval);
            return;
        }

        if (retval.trim().length > 0) {
            document.getElementById('dvContractToSign').innerHTML = "";
            var arVal = eval(retval);

            var t = document.createElement('table');
            var tb = document.createElement('tbody');
            t.className = "interview-schedule-table tbl-common-Css";

            var th = appendRow(tb, 'interview-schedule-header hdr-common-Css');

            appendCell(th, 'Vessel');
            appendCell(th, 'Staff Code');
            appendCell(th, 'Staff Name');
            appendCell(th, 'Rank');
            appendCell(th, 'Contract Date');
            appendCell(th, 'View Contract');
            tb.appendChild(th);

            for (var i = 0; i < arVal.length; i++) {
                var lstItem = arVal[i];
                var tr = appendRow(tb, "row-common-Css");
                var sLink = "";

                appendCell(tr, lstItem.VESSEL_SHORT_NAME);
                appendCell(tr, "<a href='../Crew/CrewDetails.aspx?ID=" + lstItem.CREWID + "' target='_blank'>" + lstItem.STAFF_CODE + "</a>");
                appendCell(tr, lstItem.STAFF_FULLNAME);
                appendCell(tr, lstItem.RANK_SHORT_NAME);
                appendCell(tr, lstItem.CONTRACT_DATE);
                appendCell(tr, "<a href='../Crew/CrewContract.aspx?CrewID=" + lstItem.CREWID + "&VoyID=" + lstItem.VOYAGEID + "' target='_blank'>Contract</a>");

            }

            t.appendChild(tb);

            var dvRes = document.getElementById('dvContractToSign');
            dvRes.innerHTML = "";
            dvRes.appendChild(t);

            try {
                $("#lblWebPartContractToSign").jqxWindow('setContent', document.getElementById('dvContractToSign').outerHTML);
            } catch (ex) { }

        } else {

            document.getElementById('dvContractToSign').innerHTML = "<span style='color:maroon;padding:2px'> No record found !</span>";
        }
    }
    catch (ex) { }

}


function Async_getContract_ToVerify_Alerts(userid) {
    var url = "../webservice.asmx/getContracts_ToVerify_Alerts";
    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    var params = 'userid=' + userid + '&DateFormat=' + DateFormat.toString();
    obj = new AsyncResponse(url, params, response_getContracts_ToVerify_Alerts);
    obj.getResponse();
}

function response_getContracts_ToVerify_Alerts(retval) {
    var ar, arS;

    if (retval.indexOf('Working') >= 0) { return; }
    try {

        retval = clearXMLTags(retval);
        if (retval.indexOf('ERROR:', 0) >= 0 || retval.indexOf('error', 0) >= 0) {
            alert(retval);
            return;
        }

        if (retval.trim().length > 0) {
            document.getElementById('dvContractToVerify').innerHTML = "";
            var arVal = eval(retval);

            var t = document.createElement('table');
            var tb = document.createElement('tbody');
            t.className = "interview-schedule-table tbl-common-Css";

            var th = appendRow(tb, 'interview-schedule-header hdr-common-Css');

            appendCell(th, 'Vessel');
            appendCell(th, 'Staff Code');
            appendCell(th, 'Staff Name');
            appendCell(th, 'Rank');
            appendCell(th, 'Contract Date');
            appendCell(th, 'View Contract');
            tb.appendChild(th);

            for (var i = 0; i < arVal.length; i++) {
                var lstItem = arVal[i];
                var tr = appendRow(tb, "row-common-Css");
                var sLink = "";

                appendCell(tr, lstItem.VESSEL_SHORT_NAME);
                appendCell(tr, "<a href='../Crew/CrewDetails.aspx?ID=" + lstItem.CREWID + "' target='_blank'>" + lstItem.STAFF_CODE + "</a>");
                appendCell(tr, lstItem.STAFF_FULLNAME);
                appendCell(tr, lstItem.RANK_SHORT_NAME);
                appendCell(tr, lstItem.CONTRACT_DATE);
                appendCell(tr, "<a href='../Crew/CrewContract.aspx?CrewID=" + lstItem.CREWID + "&VoyID=" + lstItem.VOYAGEID + "' target='_blank'>Contract</a>");
            }

            t.appendChild(tb);

            var dvRes = document.getElementById('dvContractToVerify');
            dvRes.innerHTML = "";
            dvRes.appendChild(t);

            try {
                $("#lblWebPartContractToVerify").jqxWindow('setContent', document.getElementById('dvContractToVerify').outerHTML);
            } catch (ex) { }

        } else {

            document.getElementById('dvContractToVerify').innerHTML = "<span style='color:maroon;padding:2px'> No record found !</span>";
        }
    }
    catch (ex) { }

}
