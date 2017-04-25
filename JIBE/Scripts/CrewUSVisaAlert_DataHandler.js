
function Async_getUSVisaAlerts(userid) {
    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    var url = "../webservice.asmx/getUSVisaAlerts";
    var params = 'userid=' + userid + '&DateFormat=' + DateFormat.toString();
    
    obj = new AsyncResponse(url, params, response_getUSVisaAlerts);
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

        if (cssClass) {
            td.className = cssClass;
        }

        tr.appendChild(td);
    }
    catch (ex) {}
    return td;
}

function response_getUSVisaAlerts(retval) {
    var ar, arS;

    if (retval.indexOf('Working') >= 0) { return; }
    try {
        
        retval = clearXMLTags(retval);
        if (retval.indexOf('ERROR:', 0) >= 0) {
            alert(retval);
            return;
        }

        if (retval.trim().length > 0) {
            //alert(retval);
            document.getElementById('dvUSVisaAlert').innerHTML = "";
            var arVal = eval(retval);
            
            var t = document.createElement('table');
            var tb = document.createElement('tbody');
            t.className = "tbl-common-Css";
            t.setAttribute("cellspacing", "0");
           
            t.setAttribute("borderColor", "#cccccc");

            var th = appendRow(tb, 'hdr-common-Css');

            appendCell(th, 'Vessel');
            appendCell(th, 'Staff Code');
            appendCell(th, 'Staff Name');
            appendCell(th, 'Rank');
            appendCell(th, 'US Visa Number');
            appendCell(th, 'US Visa Expiry Date');
            appendCell(th, 'US Visa Status');
            
            
            tb.appendChild(th);
            
            for (var i = 0; i < arVal.length; i++) {
                var lstItem = arVal[i];
                var tr = appendRow(tb, "row-common-Css");
                var sLink = "";

                appendCell(tr, lstItem.Vessel_Short_Name);
                appendCell(tr, "<a href='../Crew/CrewDetails.aspx?ID=" + lstItem.CrewID + "' target='_blank'>" + lstItem.Staff_Code + "</a>");
                appendCell(tr, lstItem.Staff_FullName);
                appendCell(tr, lstItem.Rank_Short_Name);
                appendCell(tr, lstItem.Us_Visa_Number);
                appendCell(tr, lstItem.Us_Visa_Expiry, null, lstItem.Overdue); 
                appendCell(tr, lstItem.Visa_Status, null, lstItem.Overdue);
                
            }

            t.appendChild(tb);

            var dvRes = document.getElementById('dvUSVisaAlert');
            dvRes.innerHTML = "";
            dvRes.appendChild(t);

            try {
                $("#lblWebPartUSVisaAlert").jqxWindow('setContent', document.getElementById('dvUSVisaAlert').outerHTML);
            } catch (ex) { }

            //alert(t.innerHTML);
        } else {

            document.getElementById('dvUSVisaAlert').innerHTML = "<span style='color:maroon;padding:2px'> No record found !</span>";
         
        }
    }
    catch (ex) { }

}



