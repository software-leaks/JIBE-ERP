



var ColumnCount_Supp = 7;

var SuppGridColumnCount = 25;

var UnitsPkgID = '';




function checkAvailableWidth() {

    $('.hd').css('left', document.getElementById("divInfoQTN").scrollLeft - 2 + 'px')
    $('.gtdth').css('left', document.getElementById("divInfoQTN").scrollLeft - 2 + 'px')


}


function SelectAll(qtnid, objid) {

    var checked = document.getElementById(objid.id).checked;

    var griditems = document.getElementById("ctl00_MainContent_rgdQuatationInfo");

    for (var j = 2; j <= griditems.rows.length - 1; j++) {

        var chk_id = "";


        if (j < 10) {

            chk_id = "ctl00_MainContent_rgdQuatationInfo_ctl0" + j.toString() + "_chk_" + qtnid.toString();


        }
        else {

            chk_id = "ctl00_MainContent_rgdQuatationInfo_ctl" + j.toString() + "_chk_" + qtnid.toString();

        }

        var chkobj = document.getElementById(chk_id);
        if (chkobj !=null) {
            if (chkobj.disabled != true) {

                chkobj.checked = checked;
            }
        }
    }

}



function UpdateEvalution() {

    var userID = document.getElementById("ctl00_MainContent_hdfUserIDSaveEval").value;
    var strQuery = "";
    var strQueryFinal = "";

    var griditems = document.getElementById("ctl00_MainContent_rgdQuatationInfo");

    var quotation_codes_compare = document.getElementById("ctl00_MainContent_hdfquotation_codes_compare").value; ;
    var arrquotation_codes_compare = quotation_codes_compare.split(",");

    for (var i = 0; i < (arrquotation_codes_compare.length - 1); i++) {

        // loop items

        for (var j = 2; j < griditems.rows.length; j++) {

            var chk_id_qtn = "";

            var qtn_item_id = "";

            if (j < 10) {

                chk_id = "ctl00_MainContent_rgdQuatationInfo_ctl0" + j.toString() + "_chk_" + arrquotation_codes_compare[i].toString();

                qtn_item_id = "ctl00_MainContent_rgdQuatationInfo_ctl0" + j.toString() + "_hdf_" + arrquotation_codes_compare[i].toString();

            }
            else {


                chk_id = "ctl00_MainContent_rgdQuatationInfo_ctl" + j.toString() + "_chk_" + arrquotation_codes_compare[i].toString();

                qtn_item_id = "ctl00_MainContent_rgdQuatationInfo_ctl" + j.toString() + "_hdf_" + arrquotation_codes_compare[i].toString();
            }

            var chk_id_obj = document.getElementById(chk_id);

            if (chk_id_obj != null) {
                var qtn_item_val = document.getElementById(qtn_item_id).value;

                if (chk_id_obj.checked == true && chk_id_obj.disabled != true) {

                    strQuery = " Update PURC_DTL_CTP_Quotation_Item set  Modified_By=" + userID + ", Date_Of_Modified=getdate(), Evaluated=1  where QTN_Item_ID= " + qtn_item_val + " ";

                }
                else {
                    if (chk_id_obj.disabled != true) {
                        strQuery = " Update PURC_DTL_CTP_Quotation_Item set  Modified_By=" + userID + ", Date_Of_Modified=getdate(), Evaluated=0  where QTN_Item_ID= " + qtn_item_val + " ";

                    }

                }

                strQueryFinal += strQuery;

            }
        }

    }

    document.getElementById('ctl00_MainContent_HiddenQuery').value = strQueryFinal.toString();

    return true;




}

function ShowModalFinalizeItems() {
   
    var sts= UpdateEvalution();
    showModal('dvApprovalPopUp'); 
    return false;
}