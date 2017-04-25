
function Async_InsPurchaseRemarks(DocumentCode, UserID, Remark, Remark_Type) {
    var url = "../webservice.asmx/Purc_Ins_Remarks";
    var params = 'DocCode=' + DocumentCode + '&UserID=' + UserID + '&Remark=' + Remark + '&Remark_Type=' + Remark_Type;


    obj = new AsyncResponse(url, params, response_InsPurchaseRemarks);
    obj.getResponse();

}

function response_InsPurchaseRemarks(retval) {
    var ar, arS;

    if (retval.indexOf('Working') >= 0) { return; }
    try {

        retval = clearXMLTags(retval);

        if (retval.indexOf('ERROR:', 0) >= 0 || retval.indexOf('error', 0) >= 0) {
            alert(retval);
            return;
        }

        alert('Saved successfully');

    }
   


    catch (ex) { alert(ex.message); }

}








