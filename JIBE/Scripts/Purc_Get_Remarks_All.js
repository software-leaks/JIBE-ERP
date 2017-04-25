
function Async_getPurchaseRemarksAll(DocumentCode, Remark_Type) {
    var url = "../webservice.asmx/Purc_Get_Remarks";
    var params = 'DocumentCode=' + DocumentCode + '&Remark_Type=' + Remark_Type;


    obj = new AsyncResponse(url, params, response_getPurchaseRemarksAll);
    obj.getResponse();

}

function response_getPurchaseRemarksAll(retval) {
    var ar, arS;

    if (retval.indexOf('Working') >= 0) { return; }
    try {

        retval = clearXMLTags(retval);

       

        if (retval.trim().length > 0) {

            var arVal = eval(retval);

            var t = document.createElement('table');
            var trRowHead = document.createElement('tr');
            var tdNameHead = document.createElement('td');
            var tdDateHead = document.createElement('td');
            var tdStageHead = document.createElement('td');
            var tdRemarkHead = document.createElement('td');

            tdNameHead.className = 'tdHAll';
            tdDateHead.className = 'tdHAll';
            tdStageHead.className = 'tdHAll';
            tdRemarkHead.className = 'tdHAll';


            tdNameHead.innerHTML = 'Name';
            tdDateHead.innerHTML = 'Date';
            tdStageHead.innerHTML = 'Type';
            tdRemarkHead.innerHTML = 'Remark';



            trRowHead.appendChild(tdNameHead);
            trRowHead.appendChild(tdDateHead);
            trRowHead.appendChild(tdStageHead);
            trRowHead.appendChild(tdRemarkHead);
            t.appendChild(trRowHead);

            t.className = 'tblRemarkAll';

            for (var i = 0; i < arVal.length; i++) {
               
                var trRow = document.createElement('tr');

                var tdNameDT = document.createElement('td');
                var tdDateDT = document.createElement('td');
                var tdStageDT = document.createElement('td');
                var tdRemarkDT = document.createElement('td');


                tdNameDT.innerHTML = arVal[i].name.toString();
                tdDateDT.innerHTML = arVal[i].dateCr.toString();
                tdStageDT.innerHTML = arVal[i].Stage.toString();
                tdRemarkDT.innerHTML = arVal[i].Remark.toString();

             


                tdDateDT.className = 'tdDAll';
                tdNameDT.className = 'tdDAll';
                tdStageDT.className = 'tdDAll';
                tdRemarkDT.className = 'tdDAllRemark';

             
                trRow.appendChild(tdNameDT);
                trRow.appendChild(tdDateDT);
                trRow.appendChild(tdStageDT);
                trRow.appendChild(tdRemarkDT);



                t.appendChild(trRow);

            } //for --




            var dvRes = document.getElementById('dvShowPurchaserRemark');
            dvRes.innerHTML = "";
            dvRes.innerHTML = '<table style="border-top:1px solid gray;border-right:1px solid gray"  cellpadding=0 cellspacing=0>' + t.innerHTML.toString() + '</table>';
            //dvRes.appendChild(t);


        }
        else {
            var dvRes = document.getElementById('dvShowPurchaserRemark');
            dvRes.innerHTML = "";
        }
    }
    catch (ex) { alert(ex.message); }

}








