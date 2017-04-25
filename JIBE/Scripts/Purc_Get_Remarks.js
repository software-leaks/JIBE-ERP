
function Async_getPurchaseRemarks(DocumentCode, Remark_Type) {
    var url = "../webservice.asmx/Purc_Get_Remarks";
    var params = 'DocumentCode=' + DocumentCode + '&Remark_Type=' + Remark_Type;


    obj = new AsyncResponse(url, params, response_getPurchaseRemarks);
    obj.getResponse();

}

function response_getPurchaseRemarks(retval) {
    var ar, arS;

    if (retval.indexOf('Working') >= 0) { return; }
    try {

        retval = clearXMLTags(retval);

       

        if (retval.trim().length > 0) {

            var arVal = eval(retval);

            var t = document.createElement('table');

            t.className = 'tdTooltip';

            for (var i = 0; i < arVal.length; i++) {
                var tr = document.createElement('tr');
                var td = document.createElement('td');
                var tch = document.createElement('table');

                var trName = document.createElement('tr');
                var tdNameHD = document.createElement('td');
                var tdNameDT = document.createElement('td');

                var trDate = document.createElement('tr');
                var tdDateHD = document.createElement('td');
                var tdDateDT = document.createElement('td');

                var trStage = document.createElement('tr');
                var tdStageHD = document.createElement('td');
                var tdStageDT = document.createElement('td');

                var trRemark = document.createElement('tr');
                var tdRemarkHD = document.createElement('td');
                var tdRemarkDT = document.createElement('td');

                tdNameHD.innerHTML = 'Name :';
                tdNameDT.innerHTML = arVal[i].name.toString();
                tdDateHD.innerHTML = 'Date :';
                tdDateDT.innerHTML = arVal[i].dateCr.toString();
                tdStageHD.innerHTML = "Type :";
                tdStageDT.innerHTML = arVal[i].Stage.toString();
                tdRemarkHD.innerHTML = 'Remark :';
                tdRemarkDT.innerHTML = arVal[i].Remark.toString();


                td.className = 'tdTooltip';
                tdDateHD.className = 'tdHtip';
                tdNameHD.className = 'tdHtip';
                tdStageHD.className = 'tdHtip';
                tdRemarkHD.className = 'tdHtip';

                tdDateDT.className = 'tdDtip';
                tdNameDT.className = 'tdDtip';
                tdStageDT.className = 'tdDtip';
                tdRemarkDT.className = 'tdDtip';

                tr.appendChild(td);
                td.appendChild(tch);

                tch.appendChild(trName);
                trName.appendChild(tdNameHD);
                trName.appendChild(tdNameDT);

                tch.appendChild(trDate);
                trDate.appendChild(tdDateHD);
                trDate.appendChild(tdDateDT);

                tch.appendChild(trStage);
                trStage.appendChild(tdStageHD);
                trStage.appendChild(tdStageDT);

                tch.appendChild(trRemark);
                trRemark.appendChild(tdRemarkHD);
                trRemark.appendChild(tdRemarkDT);
                
                t.appendChild(tr);

            } //for --

                      

            var dvRes = document.getElementById('dvPurchaseRemark');
            dvRes.innerHTML = "";
            dvRes.innerHTML = '<table cellpadding=0 cellspacing=0>' + t.innerHTML.toString() + '</table>';
            //dvRes.appendChild(t);


        }
        else {
            var dvRes = document.getElementById('dvPurchaseRemark');
            dvRes.innerHTML = "";
            dvRes.style.display = 'none';
        }
    }
    catch (ex) {  }

}








