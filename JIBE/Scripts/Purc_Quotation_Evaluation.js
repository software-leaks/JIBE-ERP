



var ColumnCount_Supp = 7;

var SuppGridColumnCount = 25;

var UnitsPkgID = '';




function ShowUnitsPkg(id) {

    UnitsPkgID = id; //.split("img")[0] + "lbtnUnitsPKg";
    document.getElementById("dvUnitspkg").style.display = "block";

}
function ChangeUnitsPkg() {

    var ddlUnitspk = document.getElementById("ctl00_MainContent_cmbUnitnPackage");
    var Text = ddlUnitspk.options[ddlUnitspk.selectedIndex].text;
    var Value = ddlUnitspk.options[ddlUnitspk.selectedIndex].value;
    document.getElementById(UnitsPkgID).innerHTML = Value;
    document.getElementById("dvUnitspkg").style.display = "none";
    return false;
}


function CloseDvUnits() {
    document.getElementById("dvUnitspkg").style.display = "none";
}


function checkAvailableWidth() {

    $('#divInfoQTN th').css('top', document.getElementById("divInfoQTN").scrollTop - 2 + 'px')
    $('.hd').css('left', document.getElementById("divInfoQTN").scrollLeft - 2 + 'px')
    $('.gtdth').css('left', document.getElementById("divInfoQTN").scrollLeft - 2 + 'px')


}

function FixGridheader() {
    $('#divInfoQTN th').css('top', document.getElementById("divInfoQTN").scrollTop - 2 + 'px')
}


function ShowToolTip(DivID) {
    document.getElementById('ItemDesc').style.display = "block";
    return false;

}

function HideToolTip(DivID) {
    document.getElementById('ItemDesc').style.display = "none";

}

function ItemsHistoryShow() {


    var items = document.getElementById("ctl00_MainContent_lblITEMSYSTEMCODE").value;
    var VesselCode = document.getElementById("ctl00_MainContent_lblVesselCode").value;
    window.open("Delivery_History.aspx?itemcode=" + items + "&VesselCode=" + VesselCode);
    return false;
}

function divHistoryShow() {

    document.getElementById("divHistory").style.display = "block";

    return false;

}

function DivHistoryClose() {

    document.getElementById("divHistory").style.display = "none";


}

function divReworkShow() {

    document.getElementById("dvReworkToPurc").style.display = "block";

    return false;

}

function DivReworkClose() {
    document.getElementById("dvReworkToPurc").style.display = "none";
    return false;
}

function DivReworkSuppClose() {
    document.getElementById("dvReworktoSuppler").style.display = "none";
    return false;
}
function DivReworkSuppShow() {
    document.getElementById("dvReworktoSuppler").style.display = "block";
}


function ValidationOnApprovePo() {
    var txtComment = document.getElementById("ctl00_MainContent_txtComment").value;
    var ddlBudgetCode = document.getElementById("ctl00_MainContent_ddlBudgetCode").value;

    if (ddlBudgetCode == 0) {
        alert("Select budget code");
        return false;
    }

    if (txtComment == "") {
        alert("Comments is a mandatory field.");
        return false;
    }

    return true;
}

function MaskMoney(evt) {
    if (!(evt.keyCode == 9 || evt.keyCode == 190 || evt.keyCode == 110 || evt.keyCode == 8 || evt.keyCode == 37 || evt.keyCode == 39 || evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57) || (evt.keyCode >= 96 && evt.keyCode <= 105))) {
        return false;
    }
    var parts = evt.srcElement.value.split(',');

    if (parts.length > 2) return false;
    if (evt.keyCode == 46) return (parts.length == 1);
    if (parts[0].length >= 14) return false;
    if (parts.length == 2 && parts[1].length >= 2) return false;
}

function ItemTypeChanged(ID, obj, txtORqtyClientID) {

    var ddlitemtype_id = obj.id;
    var arrddlitemtype_id = ddlitemtype_id.split('_');

    var prefix_rate_id = arrddlitemtype_id[0].toString() + "_" + arrddlitemtype_id[1].toString() + "_" + arrddlitemtype_id[2].toString() + "_" + arrddlitemtype_id[3].toString();
    var rate_id = "";
    var chk_id = "";
    var quotation_codes_compare = document.getElementById("ctl00_MainContent_hdfquotation_codes_compare").value; ;
    var arrquotation_codes_compare = quotation_codes_compare.split(",");
    for (var j = 0; j < arrquotation_codes_compare.length; j++) {

        if (ddlitemtype_id.search(arrquotation_codes_compare[j].toString()) != -1) {

            rate_id = prefix_rate_id + "_" + arrquotation_codes_compare[j].toString() + "_UnitPrice";
            chk_id = prefix_rate_id + "_" + arrquotation_codes_compare[j].toString();
        }
    }

    var rate_obj = document.getElementById(rate_id);

    var chkID = parseInt(ID) + 6;
    if (document.getElementById(chk_id).checked) {

        rate_obj.innerHTML = obj.value.split(",")[0];

        Calculate_ordqty_rate_changed(null, txtORqtyClientID);

    }
    else
        alert("Please select item!")



}

function Calculate_ordqty_rate_changed(evt, isordqtyid) {

    var OrdQtyID = "";
    if (!isordqtyid) {

        var ev = (window.event) ? event : evt;
        var OrdQtyobj = (ev.srcElement) ? ev.srcElement : ev.target;
        OrdQtyID = OrdQtyobj.id;
    }
    else {
        OrdQtyID = isordqtyid.id;
    }

    var ordqty_val = document.getElementById(OrdQtyID).value;
    var quotation_codes_compare = document.getElementById("ctl00_MainContent_hdfquotation_codes_compare").value; ;
    var arrquotation_codes_compare = quotation_codes_compare.split(",");
    var arrtxt_id = OrdQtyID.split('_');

    var prefix_id = arrtxt_id[0].toString() + "_" + arrtxt_id[1].toString() + "_" + arrtxt_id[2].toString() + "_" + arrtxt_id[3].toString();

    for (var j = 0; j < arrquotation_codes_compare.length; j++) {

        var txtDiscount = document.getElementById(prefix_id + "_" + arrquotation_codes_compare[j].toString() + "_Discount").innerHTML;
        var txtRate = document.getElementById(prefix_id + "_" + arrquotation_codes_compare[j].toString() + "_UnitPrice").innerHTML;
        var iQty = (ordqty_val.strim() == "") ? 0 : ordqty_val;
        var iDiscount = (txtDiscount.strim() == "") ? 0 : txtDiscount;
        var iRate = (txtRate.strim() == "") ? 0 : txtRate;
        if (iRate != 0) {
            document.getElementById(prefix_id + "_" + arrquotation_codes_compare[j].toString() + "_Amount").innerHTML = ((iQty * iRate) - ((iQty * iDiscount * iRate) / 100)).toFixed(2);
        }

    }

    calculate();

}

function SelectAll(id, columnId) {

    var checked = document.getElementById(id).checked;

    var griditems = document.getElementById("ctl00_MainContent_rgdQuatationInfo");
    var quotation_codes_compare = document.getElementById("ctl00_MainContent_hdfquotation_codes_compare").value; ;
    var arrquotation_codes_compare = quotation_codes_compare.split(",");

    var arrchk_id = id.split('_');
    var ckk_id_postfix = "";
    for (var m = 0; m < arrquotation_codes_compare.length; m++) {

        if (id.search(arrquotation_codes_compare[m].toString()) != -1) {

            ckk_id_postfix = arrquotation_codes_compare[m].toString();

        }
    }



    for (var i = 1; i <= griditems.rows.length - 1; i++) {

        var chk_id = "";
        var chk_id_other = "";
        var UnitPrice_id = "";

        if (i < 10) {

            chk_id = "ctl00_MainContent_rgdQuatationInfo_ctl0" + i.toString() + "_" + ckk_id_postfix;
            chk_id_other = "ctl00_MainContent_rgdQuatationInfo_ctl0" + i.toString();

        }
        else {

            chk_id = "ctl00_MainContent_rgdQuatationInfo_ctl" + i.toString() + "_" + ckk_id_postfix;
            chk_id_other = "ctl00_MainContent_rgdQuatationInfo_ctl" + i.toString();

        }

        if (i != 1)
            UnitPrice_id = i < 10 ? "ctl00_MainContent_rgdQuatationInfo_ctl0" + i.toString() + "_" + ckk_id_postfix + "_UnitPrice" : "ctl00_MainContent_rgdQuatationInfo_ctl" + i.toString() + "_" + ckk_id_postfix + "_UnitPrice"; ;

        for (var j = 0; j < arrquotation_codes_compare.length; j++) {
            if (checked == true) {
                var chk_id_other_local = chk_id_other;
                if (arrquotation_codes_compare[j].toString() != ckk_id_postfix) {

                    chk_id_other_local = chk_id_other_local + "_" + arrquotation_codes_compare[j].toString();
                    document.getElementById(chk_id_other_local).checked = (checked == true) ? false : true;

                }
            }

        }

        if (document.getElementById(chk_id).disabled != true) {
            if (i != 1) {
                if (parseFloat(document.getElementById(UnitPrice_id).innerHTML) > 0)
                    document.getElementById(chk_id).checked = checked;
            }
            else
                document.getElementById(chk_id).checked = checked;
        }
    }
    // call the calculate method
    calculate();



}

function Fill_QrdQty_With_ReqQty() {//only where ordered qty is zero
    var cnfm = confirm('You have selected to copy the request qty to order qty.\n  Are you sure ?');

    if (cnfm == true) {
        var griditems = document.getElementById("ctl00_MainContent_rgdQuatationInfo");


        for (var i = 2; i <= griditems.rows.length - 1; i++) {

            var reqqty_id = "";
            var ordqty_id = "";
            var ordqty_obj = "";

            if (i < 10) {

                reqqty_id = "ctl00_MainContent_rgdQuatationInfo_ctl0" + i.toString() + "_lblReqs_tQty";
                ordqty_id = "ctl00_MainContent_rgdQuatationInfo_ctl0" + i.toString() + "_txtORqty";
            }
            else {

                reqqty_id = "ctl00_MainContent_rgdQuatationInfo_ctl" + i.toString() + "_lblReqs_tQty";
                ordqty_id = "ctl00_MainContent_rgdQuatationInfo_ctl" + i.toString() + "_txtORqty";
            }

            ordqty_obj = document.getElementById(ordqty_id);
            if (parseFloat(ordqty_obj.value) == 0) {

                ordqty_obj.value = document.getElementById(reqqty_id).innerHTML;
            }
        }
    }
    else {
        return false;
    }

    calculate();
}


function SelectRowsClick(id, RowId, columnId) {

    var checked = document.getElementById(id).checked;

    var quotation_codes_compare = document.getElementById("ctl00_MainContent_hdfquotation_codes_compare").value; ;
    var arrquotation_codes_compare = quotation_codes_compare.split(",");

    var arrchk_id = id.split('_');
    var ckk_id_postfix_qtn = "";

    for (var m = 0; m < arrquotation_codes_compare.length; m++) {

        if (id.search(arrquotation_codes_compare[m].toString()) != -1) {

            ckk_id_postfix_qtn = arrquotation_codes_compare[m].toString();

        }
    }


    for (var j = 0; j < arrquotation_codes_compare.length; j++) {


        var chk_id_other = arrchk_id[0].toString() + "_" + arrchk_id[1].toString() + "_" + arrchk_id[2].toString() + "_" + arrchk_id[3].toString();

        if (checked) {

            if (arrquotation_codes_compare[j].toString() != ckk_id_postfix_qtn) {

                document.getElementById(chk_id_other + "_" + arrquotation_codes_compare[j].toString()).checked = (checked == true) ? false : true;

            }
        }

    }

    var unit_price = id + "_UnitPrice";
    if (parseFloat(document.getElementById(unit_price).innerHTML) > 0) {

        if (checked) {
            var ord_qty_id = chk_id_other + "_txtORqty";
            var ord_qty = document.getElementById(ord_qty_id);
            var lblReqs_tQty = document.getElementById(chk_id_other + "_lblReqs_tQty");
            ord_qty.value = (parseFloat(ord_qty.value) == 0) ? lblReqs_tQty.innerHTML : ord_qty.value;
        }
        document.getElementById(id).checked = checked;
    }
    else
        document.getElementById(id).checked = (checked == true) ? false : true;


    // call the calculate method
    calculate();

}




function calculate() {

    document.getElementById('ctl00_MainContent_HiddenFieldTotalAmountApproved').value = "";

    var chk_all_ColumnID = "";
    var griditems = document.getElementById("ctl00_MainContent_rgdQuatationInfo");
    var suppliergrid = document.getElementById("ctl00_MainContent_rgdSupplierInfo");
    var quotation_codes_compare = document.getElementById("ctl00_MainContent_hdfquotation_codes_compare").value; ;
    var arrquotation_codes_compare = quotation_codes_compare.split(",");

    for (var i = 0; i < arrquotation_codes_compare.length; i++) {

        var Tot_Items = 0;
        var Tot_Amount = 0;
        var net_Amount = 0;

        chk_all_ColumnID = "ctl00_MainContent_rgdQuatationInfo_ctl01" + "_" + arrquotation_codes_compare[i].toString();

        for (var j = 2; j < griditems.rows.length; j++) {

            var amt_id = "";
            var chk_id_qtn = "";

            if (j < 10) {

                amt_id = "ctl00_MainContent_rgdQuatationInfo_ctl0" + j.toString() + "_" + arrquotation_codes_compare[i].toString() + "_Amount";
                chk_id_qtn = "ctl00_MainContent_rgdQuatationInfo_ctl0" + j.toString() + "_" + arrquotation_codes_compare[i].toString();


            }
            else {

                amt_id = "ctl00_MainContent_rgdQuatationInfo_ctl" + j.toString() + "_" + arrquotation_codes_compare[i].toString() + "_Amount";
                chk_id_qtn = "ctl00_MainContent_rgdQuatationInfo_ctl" + j.toString() + "_" + arrquotation_codes_compare[i].toString();
            }

            var chk_id_obj = document.getElementById(chk_id_qtn);
            if (chk_id_obj.checked == true && chk_id_obj.disabled != true) {

                Tot_Amount = Tot_Amount + parseFloat(document.getElementById(amt_id).innerHTML);
                Tot_Items = Tot_Items + 1;
            }


        }

        var vat = 0;
        var Surcharge = 0;
        var Discount = 0;
        var PkgHald = 0;
        var freightCost = 0;
        var OthCost = 0;
        var TruckCost = 0;
        var bargeCost = 0;
        var ExchRate = 1;

        var Quotation_code_suppgrid = "";

        if (Tot_Amount != 0) {

            for (var k = 2; k <= suppliergrid.rows.length; k++) {

                Quotation_code_suppgrid = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_hdfQUOTATION_CODE" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_hdfQUOTATION_CODE";

                var Quotation_code_suppgrid_val = document.getElementById(Quotation_code_suppgrid).value.toString().replace(/-/gi, '_');
                if (chk_all_ColumnID.search(Quotation_code_suppgrid_val) != -1) {

                    var vat_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_hdfVAT" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_hdfVAT";
                    var Surcharge_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_hdfSURCHARGES" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_hdfSURCHARGES";
                    var Discount_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_hdfDISCOUNT" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_hdfDISCOUNT";
                    var PkgHald_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_lblPkg" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_lblPkg";
                    var freightCost_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_lblFreight_Cost" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_lblFreight_Cost";
                    var OthCost_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_lblOtherCost" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_lblOtherCost";
                    var TruckCost_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_lblTruck_Cost" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_lblTruck_Cost";
                    var bargeCost_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_lblBarge_Workboat_Cost" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_lblBarge_Workboat_Cost";
                    var ExchRate_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_hdfEXCHANGE_RATE" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_hdfEXCHANGE_RATE";

                    var lblTot_Items_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_txtTotalItem" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_txtTotalItem";
                    var lblTot_Amount_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_txtAmount" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_txtAmount";
                    var lblvat_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_txtVat" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_txtVat";
                    var lbltxtGrandTotal_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_txtGrandTotal" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_txtGrandTotal";

                    ExchRate = document.getElementById(ExchRate_id).value;
                    Discount = (((Tot_Amount / ExchRate) * document.getElementById(Discount_id).value) / 100) * ExchRate;
                    Surcharge = (Tot_Amount - Discount) * document.getElementById(Surcharge_id).value / 100;
                    PkgHald = document.getElementById(PkgHald_id).innerHTML;
                    freightCost = document.getElementById(freightCost_id).innerHTML;
                    OthCost = document.getElementById(OthCost_id).innerHTML;
                    TruckCost = document.getElementById(TruckCost_id).innerHTML;
                    bargeCost = document.getElementById(bargeCost_id).innerHTML;
                    vat = (((Tot_Amount / ExchRate) - (Discount / ExchRate) + Surcharge) * document.getElementById(vat_id).value / 100) * ExchRate;
                    net_Amount = (Tot_Amount - Discount + Surcharge + vat + parseFloat(PkgHald) + parseFloat(freightCost) + parseFloat(OthCost) + parseFloat(TruckCost) + parseFloat(bargeCost)).toFixed(2);

                    document.getElementById(lblTot_Items_id).innerHTML = Tot_Items.toFixed(2);
                    document.getElementById(lblTot_Amount_id).innerHTML = Tot_Amount.toFixed(2);

                    document.getElementById(lblvat_id).innerHTML = vat.toFixed(2);
                    document.getElementById(lbltxtGrandTotal_id).innerHTML = net_Amount;

                    var arrqtn_code = Quotation_code_suppgrid_val.split("QT");
                    document.getElementById('ctl00_MainContent_HiddenFieldTotalAmountApproved').value += net_Amount + '&' + "QT" + arrqtn_code[1].toString().replace(/_/gi, '-') + '@';

                    var QtnCode_FinalAmount = document.getElementById("ctl00_MainContent_hdf_QtnCode_FinalAmount");
                    var firstTime_clicked = document.getElementById("ctl00_MainContent_hdf_firstTime_clicked").value;
                    if (firstTime_clicked == 1) {

                        QtnCode_FinalAmount.value += net_Amount + '&' + "QT" + arrqtn_code[1].toString().replace(/_/gi, '-') + '@';

                    }


                }

            }
        }
        else {



            for (var k = 2; k <= suppliergrid.rows.length; k++) {

                Quotation_code_suppgrid = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_hdfQUOTATION_CODE" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_hdfQUOTATION_CODE";

                var Quotation_code_suppgrid_val = document.getElementById(Quotation_code_suppgrid).value.toString().replace(/-/gi, '_');
                if (chk_all_ColumnID.search(Quotation_code_suppgrid_val) != -1) {


                    var lblTot_Items_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_txtTotalItem" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_txtTotalItem";
                    var lblTot_Amount_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_txtAmount" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_txtAmount";
                    var lblvat_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_txtVat" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_txtVat";
                    var lbltxtGrandTotal_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_txtGrandTotal" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_txtGrandTotal";


                    document.getElementById(lblTot_Items_id).innerHTML = 0;
                    document.getElementById(lblTot_Amount_id).innerHTML = 0;

                    document.getElementById(lblvat_id).innerHTML = 0;
                    document.getElementById(lbltxtGrandTotal_id).innerHTML = 0;

                }

            }

        }

    }

    document.getElementById("ctl00_MainContent_hdf_firstTime_clicked").value = "0";

}



function CloseDiv() {
    var control = document.getElementById("ctl00_MainContent_divOnSplit");
    control.style.visibility = "hidden";
}



function UpdateEvalution() {


    document.getElementById('ctl00_MainContent_hdfSupplierBeingApproved').value = "";
    var userID = document.getElementById("ctl00_MainContent_hdfUserIDSaveEval").value;
    var strQuery = "";
    var strQueryFinal = "";
    var strItemRefCodes = "";
    var DocumentCode = document.getElementById("ctl00_MainContent_HiddenDocumentCode").value;


    var griditems = document.getElementById("ctl00_MainContent_rgdQuatationInfo");
    var suppliergrid = document.getElementById("ctl00_MainContent_rgdSupplierInfo");
    var quotation_codes_compare = document.getElementById("ctl00_MainContent_hdfquotation_codes_compare").value; ;
    var arrquotation_codes_compare = quotation_codes_compare.split(",");
    var chkCount = 0;

    for (var i = 0; i < arrquotation_codes_compare.length; i++) {

        //get the supp code and qtn code from supplier grid

        var QUOTATION_CODE = "";
        var supplier_code = "";
        var currentSupplier = "";
        var isthisSupplierSelected = 0;
        var ITEM_REF_CODE = 0;

        for (var k = 2; k <= suppliergrid.rows.length; k++) {

            var Quotation_code_suppgrid = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_hdfQUOTATION_CODE" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_hdfQUOTATION_CODE";
            var supplier_code_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_hdfSupplier_code" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_hdfSupplier_code";

            var Quotation_code_suppgrid_val = document.getElementById(Quotation_code_suppgrid).value.toString().replace(/-/gi, '_');
            if (arrquotation_codes_compare[i].toString().search(Quotation_code_suppgrid_val) != -1) {

                QUOTATION_CODE = Quotation_code_suppgrid_val.replace(/_/gi, '-');
                currentSupplier = document.getElementById(supplier_code_id).value;
                supplier_code = document.getElementById(supplier_code_id).value;

            }
        }

        // loop items

        for (var j = 2; j < griditems.rows.length; j++) {


            var chk_id_qtn = "";
            var Rate_id = "";
            var itemtype_id = "";
            var ordqty_id = "";
            var ordunit_id = "";
            var itemrefcode_id = "";


            if (j < 10) {


                chk_id_qtn = "ctl00_MainContent_rgdQuatationInfo_ctl0" + j.toString() + "_" + arrquotation_codes_compare[i].toString();
                Rate_id = "ctl00_MainContent_rgdQuatationInfo_ctl0" + j.toString() + "_" + arrquotation_codes_compare[i].toString() + "_UnitPrice";
                itemtype_id = "ctl00_MainContent_rgdQuatationInfo_ctl0" + j.toString() + "_" + arrquotation_codes_compare[i].toString() + "_ddl";
                ordqty_id = "ctl00_MainContent_rgdQuatationInfo_ctl0" + j.toString() + "_txtORqty";
                ordunit_id = "ctl00_MainContent_rgdQuatationInfo_ctl0" + j.toString() + "_lbtnUnitsPKg";
                itemrefcode_id = "ctl00_MainContent_rgdQuatationInfo_ctl0" + j.toString() + "_hdfItemRef_Code";

            }
            else {


                chk_id_qtn = "ctl00_MainContent_rgdQuatationInfo_ctl" + j.toString() + "_" + arrquotation_codes_compare[i].toString();
                Rate_id = "ctl00_MainContent_rgdQuatationInfo_ctl" + j.toString() + "_" + arrquotation_codes_compare[i].toString() + "_UnitPrice";
                itemtype_id = "ctl00_MainContent_rgdQuatationInfo_ctl" + j.toString() + "_" + arrquotation_codes_compare[i].toString() + "_ddl";
                ordqty_id = "ctl00_MainContent_rgdQuatationInfo_ctl" + j.toString() + "_txtORqty";
                ordunit_id = "ctl00_MainContent_rgdQuatationInfo_ctl" + j.toString() + "_lbtnUnitsPKg";
                itemrefcode_id = "ctl00_MainContent_rgdQuatationInfo_ctl" + j.toString() + "_hdfItemRef_Code";
            }

            var chk_id_obj = document.getElementById(chk_id_qtn);
            var itemrefcode_val = document.getElementById(itemrefcode_id).value;

            if (chk_id_obj.checked == true && chk_id_obj.disabled != true) {


                var strtxtORqty = document.getElementById(ordqty_id).value;
                var strtxtOunit = document.getElementById(ordunit_id).innerHTML;
                var strRate = 0;
                var strItemType = 0;

                strRate = document.getElementById(Rate_id).innerHTML;
                // this item has not been sent to this supplier for quotation
                if (parseFloat(strRate) != 0) {
                    strItemType = document.getElementById(itemtype_id).options[document.getElementById(itemtype_id).selectedIndex].value.split(",")[1];

                    strQuery = "Update PURC_Dtl_Quoted_Prices set  Modified_By=" + userID + ", Date_Of_Modified=getdate(), EVALUATION_OPTION='1' ,SYNC_FLAG='0', Quoted_Rate=(select isnull(Quoted_Rate,(select isnull(QUOTED_RATE,0) from PURC_DTL_QUOTED_PRICES where QUOTATION_CODE='" + QUOTATION_CODE + "' and ITEM_REF_CODE='" + itemrefcode_val + "' and SUPPLIER_CODE='" + supplier_code + "')) from PURC_DTL_QuotedPrices_ItemType where Quotation_Code='" + QUOTATION_CODE + "' and  Item_Ref_Code='" + itemrefcode_val + "' and Item_Type='" + strItemType + "' ) , Item_Type = '" + strItemType + "'  where SUPPLIER_CODE= '" + supplier_code + "' and QUOTATION_CODE='" + QUOTATION_CODE + "' and ITEM_REF_CODE ='" + itemrefcode_val + "' and DOCUMENT_CODE='" + DocumentCode + "' "

                    strQuery = strQuery + "Update PURC_Dtl_Quoted_Prices set  Modified_By=" + userID + ", Date_Of_Modified=getdate(), EVALUATION_OPTION='0' ,SYNC_FLAG='0' where  QUOTATION_CODE !='" + QUOTATION_CODE + "' and ITEM_REF_CODE ='" + itemrefcode_val + "' and DOCUMENT_CODE='" + DocumentCode + "' ";

                    strQuery = strQuery + "  Update dbo.PURC_Dtl_Supply_Items Set Modified_By=" + userID + ", Date_Of_Modification=getdate(),Order_QTY='" + strtxtORqty + "' ,SYNC_FLAG='0',ORDER_UNIT_ID='" + strtxtOunit + "'where ITEM_REF_CODE ='" + itemrefcode_val + "' and DOCUMENT_CODE='" + DocumentCode + "' "
                }
                if (strItemRefCodes.search(itemrefcode_val) == -1)
                    strItemRefCodes = strItemRefCodes + "," + itemrefcode_val;
                chkCount = chkCount + 1;
                if (isthisSupplierSelected == 0) {
                    isthisSupplierSelected = 1;
                }



            }
            else {
                if (chk_id_obj.disabled != true) {
                    strQuery = "Update PURC_Dtl_Quoted_Prices set Modified_By=" + userID + ", Date_Of_Modified=getdate(), EVALUATION_OPTION='0' ,SYNC_FLAG='0' where SUPPLIER_CODE= '" + supplier_code + "' and QUOTATION_CODE='" + QUOTATION_CODE + "' and ITEM_REF_CODE ='" + itemrefcode_val + "' and DOCUMENT_CODE='" + DocumentCode + "' "

                    if (strItemRefCodes.search(itemrefcode_val) == -1) {
                        strItemRefCodes = strItemRefCodes + "," + itemrefcode_val;
                        strQuery = strQuery + " Update dbo.PURC_Dtl_Supply_Items Set Modified_By=" + userID + ", Date_Of_Modification=getdate(),Order_QTY=0 ,SYNC_FLAG=0  where ITEM_REF_CODE ='" + itemrefcode_val + "' and DOCUMENT_CODE='" + DocumentCode + "'  "
                    }
                }

            }



            if (isthisSupplierSelected != 0) {
                if (document.getElementById('ctl00_MainContent_hdfSupplierBeingApproved').value.search(currentSupplier) == -1) {
                    document.getElementById('ctl00_MainContent_hdfSupplierBeingApproved').value += currentSupplier + ",";
                }
            }
            strQueryFinal += strQuery;



        }

    }


    if (chkCount == 0) {
        strQueryFinal = "";
        alert("Please select item !")
        return false;
    }

    var str = strQueryFinal.toString();

    document.getElementById('ctl00_MainContent_HiddenQuery').value = str;

    return true;




}


function CalculateByEvalOpt(EvalOpt, Colid) {


    switch (EvalOpt) {

        case "0":

            ////////////////// find the cheapest supplier
            document.getElementById('ctl00_MainContent_HiddenFieldTotalAmountApproved').value = "";
            var MinValues = 0.00;
            var chk_all_ColumnID = "";
            var griditems = document.getElementById("ctl00_MainContent_rgdQuatationInfo");
            var suppliergrid = document.getElementById("ctl00_MainContent_rgdSupplierInfo");
            var quotation_codes_compare = document.getElementById("ctl00_MainContent_hdfquotation_codes_compare").value; ;
            var arrquotation_codes_compare = quotation_codes_compare.split(",");

            for (var i = 0; i < arrquotation_codes_compare.length; i++) {

                var Tot_Items = 0;
                var Tot_Amount = 0;
                var net_Amount = 0;

                chk_all_ColumnID = "ctl00_MainContent_rgdQuatationInfo_ctl01" + "_" + arrquotation_codes_compare[i].toString();

                for (var j = 2; j < griditems.rows.length; j++) {

                    var amt_id = "";
                    if (j < 10) {

                        amt_id = "ctl00_MainContent_rgdQuatationInfo_ctl0" + j.toString() + "_" + arrquotation_codes_compare[i].toString() + "_Amount";

                    }
                    else {

                        amt_id = "ctl00_MainContent_rgdQuatationInfo_ctl" + j.toString() + "_" + arrquotation_codes_compare[i].toString() + "_Amount";

                    }

                    Tot_Amount = Tot_Amount + parseFloat(document.getElementById(amt_id).innerHTML);


                }

                var vat = 0;
                var Surcharge = 0;
                var Discount = 0;
                var PkgHald = 0;
                var freightCost = 0;
                var OthCost = 0;
                var TruckCost = 0;
                var bargeCost = 0;
                var ExchRate = 1;

                var Quotation_code_suppgrid = "";

                if (Tot_Amount > 0) {

                    for (var k = 2; k <= suppliergrid.rows.length; k++) {

                        Quotation_code_suppgrid = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_hdfQUOTATION_CODE" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_hdfQUOTATION_CODE";

                        var Quotation_code_suppgrid_val = document.getElementById(Quotation_code_suppgrid).value.toString().replace(/-/gi, '_');
                        if (chk_all_ColumnID.search(Quotation_code_suppgrid_val) != -1) {

                            var vat_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_hdfVAT" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_hdfVAT";
                            var Surcharge_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_hdfSURCHARGES" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_hdfSURCHARGES";
                            var Discount_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_hdfDISCOUNT" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_hdfDISCOUNT";
                            var PkgHald_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_lblPkg" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_lblPkg";
                            var freightCost_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_lblFreight_Cost" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_lblFreight_Cost";
                            var OthCost_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_lblOtherCost" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_lblOtherCost";
                            var TruckCost_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_lblTruck_Cost" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_lblTruck_Cost";
                            var bargeCost_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_lblBarge_Workboat_Cost" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_lblBarge_Workboat_Cost";
                            var ExchRate_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_hdfEXCHANGE_RATE" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_hdfEXCHANGE_RATE";


                            ExchRate = document.getElementById(ExchRate_id).value;
                            Discount = (((Tot_Amount / ExchRate) * document.getElementById(Discount_id).value) / 100) * ExchRate;
                            Surcharge = (Tot_Amount - Discount) * document.getElementById(Surcharge_id).value / 100;
                            PkgHald = document.getElementById(PkgHald_id).innerHTML;
                            freightCost = document.getElementById(freightCost_id).innerHTML;
                            OthCost = document.getElementById(OthCost_id).innerHTML;
                            TruckCost = document.getElementById(TruckCost_id).innerHTML;
                            bargeCost = document.getElementById(bargeCost_id).innerHTML;
                            vat = (((Tot_Amount / ExchRate) - (Discount / ExchRate) + Surcharge) * document.getElementById(vat_id).value / 100) * ExchRate;

                        }

                    }
                }
                net_Amount = (Tot_Amount - Discount + Surcharge + vat + parseFloat(PkgHald) + parseFloat(freightCost) + parseFloat(OthCost) + parseFloat(TruckCost) + parseFloat(bargeCost)).toFixed(2);

                if ((parseFloat(MinValues) == 0 || parseFloat(MinValues) > parseFloat(net_Amount)) && parseFloat(net_Amount) > 0) {

                    document.getElementById('ctl00_MainContent_HiddenChepSupp').value = chk_all_ColumnID;

                    MinValues = net_Amount;

                }


            }
            document.getElementById(document.getElementById('ctl00_MainContent_HiddenChepSupp').value).checked = true;
            SelectAll(document.getElementById('ctl00_MainContent_HiddenChepSupp').value, 0);


            return true;
            break;

        case "1":


            if (document.getElementById('ctl00_MainContent_HiddenChepSupp').value.length > 0)
                document.getElementById(document.getElementById('ctl00_MainContent_HiddenChepSupp').value).checked = false;

            var griditems = document.getElementById("ctl00_MainContent_rgdQuatationInfo");
            var quotation_codes_compare = document.getElementById("ctl00_MainContent_hdfquotation_codes_compare").value;
            var arrquotation_codes_compare = quotation_codes_compare.split(",");

            var arrquotation_codes_RowNum_compare = document.getElementById("ctl00_MainContent_hdfquotation_codes_RowNum_compare").value.split(",");

            for (var i = 2; i < griditems.rows.length; i++) {

                var MinAmount = 0.00;
                var Sup_Amount = 0.00;
                var Min_chkamt_id = "";


                var order_qty_id = (i < 10) ? "ctl00_MainContent_rgdQuatationInfo_ctl0" + i.toString() + "_txtORqty" : "ctl00_MainContent_rgdQuatationInfo_ctl" + i.toString() + "_txtORqty";

                for (var j = 0; j < arrquotation_codes_compare.length; j++) {

                    var amt_id = "";
                    var chkamt_id = "";
                    var UnitPrice_id = "";
                    var Discount_on_Total_id = "";
                    var supgrid_RowNum = 2 + parseInt(arrquotation_codes_RowNum_compare[j].split("!")[1].toString());



                    if (arrquotation_codes_compare[j].toString() == arrquotation_codes_RowNum_compare[j].split("!")[0].toString()) {

                        Discount_on_Total_id = (supgrid_RowNum < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + supgrid_RowNum.toString() + "_hdfDISCOUNT" : "ctl00_MainContent_rgdSupplierInfo_ctl" + supgrid_RowNum.toString() + "_hdfDISCOUNT";


                    }


                    if (i < 10) {

                        amt_id = "ctl00_MainContent_rgdQuatationInfo_ctl0" + i.toString() + "_" + arrquotation_codes_compare[j].toString() + "_Amount";
                        chkamt_id = "ctl00_MainContent_rgdQuatationInfo_ctl0" + i.toString() + "_" + arrquotation_codes_compare[j].toString();
                        UnitPrice_id = "ctl00_MainContent_rgdQuatationInfo_ctl0" + i.toString() + "_" + arrquotation_codes_compare[j].toString() + "_UnitPrice";

                    }
                    else {

                        amt_id = "ctl00_MainContent_rgdQuatationInfo_ctl" + i.toString() + "_" + arrquotation_codes_compare[j].toString() + "_Amount";
                        chkamt_id = "ctl00_MainContent_rgdQuatationInfo_ctl" + i.toString() + "_" + arrquotation_codes_compare[j].toString();
                        UnitPrice_id = "ctl00_MainContent_rgdQuatationInfo_ctl" + i.toString() + "_" + arrquotation_codes_compare[j].toString() + "_UnitPrice";

                    }

                    if (parseFloat(document.getElementById(UnitPrice_id).innerHTML) > 0) {

                        Sup_Amount = parseFloat(document.getElementById(amt_id).innerHTML) - parseFloat((parseFloat(document.getElementById(amt_id).innerHTML) * (parseFloat(document.getElementById(Discount_on_Total_id).value) / 100))).toFixed(2);

                        document.getElementById(chkamt_id).checked = false;

                        if (parseFloat(MinAmount) == 0 || parseFloat(MinAmount) > parseFloat(Sup_Amount)) {

                            Min_chkamt_id = chkamt_id;
                            MinAmount = Sup_Amount;
                        }
                    }

                }


                if (Min_chkamt_id != "") {
                    if (parseFloat(document.getElementById(order_qty_id).value) > 0 && document.getElementById(Min_chkamt_id).disabled != true)
                        document.getElementById(Min_chkamt_id).checked = true;
                }
            }

            calculate();
            return true;
            break;
    }

}





function GetSupplierStatus(SuppCode, id) 
{

//    var ev = window.event;
//    var obj = ev.srcElement;
    var ChekVal = document.getElementById(id).checked;
    if (ChekVal) 
    {
        Async_Get_Supplier_Status(SuppCode);
    }
}



function response_GetSupplierStatus(retval) {

    var ar, arS;

    if (retval.indexOf('Working') >= 0) { return; }
    try {

        retval = clearXMLTags(retval);

        if (retval.indexOf('ERROR:', 0) >= 0 || retval.indexOf('error', 0) >= 0) {
            alert(retval);
            return;
        }

        if (retval == 0)
            alert("This supplier is expired !");
        else if (retval == 2)
            alert("This supplier is  blacklisted !");
        else if (retval == 3)
            alert("This supplier is  deleted !");
        else if (retval == 4)
            alert("This supplier is  conditional !");
        else if (retval == 5)
            alert("This supplier is  unregistered !");
    }



    catch (ex) { alert(ex.message); }

}

function check_changesOnUI(functionname_id, isserver) {
    try {
        var changed = false;
        var Quotation_code_suppgrid = "";
        var suppliergrid = document.getElementById("ctl00_MainContent_rgdSupplierInfo");
        var arrQtnCode_FinalAmount = document.getElementById("ctl00_MainContent_hdf_QtnCode_FinalAmount").value.split("@");
        var QtnCode_FinalAmount = document.getElementById("ctl00_MainContent_hdf_QtnCode_FinalAmount").value;
        var chkQuaEvaluated_id = "";


        for (var k = 2; k <= suppliergrid.rows.length; k++) {

            Quotation_code_suppgrid = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_hdfQUOTATION_CODE" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_hdfQUOTATION_CODE";
            chkQuaEvaluated_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_chkQuaEvaluated" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_chkQuaEvaluated";

            if (document.getElementById(chkQuaEvaluated_id).checked == true) {

                var Quotation_code_suppgrid_val = document.getElementById(Quotation_code_suppgrid).value.toString();
                var lbltxtGrandTotal_id = "";

                for (var x = 0; x < arrQtnCode_FinalAmount.length - 1; x++) {

                    if (Quotation_code_suppgrid_val == arrQtnCode_FinalAmount[x].toString().split("&")[1].toString()) {

                        lbltxtGrandTotal_id = (k < 10) ? "ctl00_MainContent_rgdSupplierInfo_ctl0" + k.toString() + "_txtGrandTotal" : "ctl00_MainContent_rgdSupplierInfo_ctl" + k.toString() + "_txtGrandTotal";

                        if (parseFloat(document.getElementById(lbltxtGrandTotal_id).innerHTML) != parseFloat(arrQtnCode_FinalAmount[x].toString().split("&")[0].toString())) {

                            changed = true;


                        }
                    }
                }


            }

        }
    } catch (ex) {
    alert(ex.Message);
    }
    if (changed == true) {

     

        //        var r = confirm('You have chosen to refresh this quotation display. \n \n Do you want to save the changes you have made? \n \n');
        //        if (r) {
        //            var stsupd = UpdateEvalution();

        //            if (stsupd) {
        //                var postBackstr = "__doPostBack('ctl00$MainContent$btnSaveEvaln','ctl00$MainContent$btnSaveEvaln_Click')";
        //                window.setTimeout(postBackstr, 0, 'JavaScript');

        //            }
        //        }

        $.alerts.okButton = " Yes ";
        $.alerts.cancelButton = " No ";
       
        var aa = jConfirm('You have chosen to refresh this quotation display. \n \n Do you want to save the changes you have made? \n \n', ' Confirmation Required !', function (r) {

            if (r) {

                var stsupd = UpdateEvalution();

                if (stsupd == true) {
                    var postBackstr = "__doPostBack('ctl00$MainContent$btnSaveEvaln','ctl00$MainContent$btnSaveEvaln_Click')";
                    $('#ctl00_MainContent_btnSaveEvaln').click();
                   // window.setTimeout(postBackstr, 0, 'JavaScript');
                    return true;

                }
            }
            else {


                if (functionname_id != null && isserver == '1') {

                    var postBackstr = "__doPostBack('" + functionname_id.replace(/_/gi, '$') + "','" + functionname_id.replace(/_/gi, '$') + "_Click')";
                    $('#' + functionname_id).click();
                    //window.setTimeout(postBackstr, 0, 'JavaScript');
                    return true;
                }
                else if (functionname_id != null && isserver == '0') {
                   
                    window.setTimeout(functionname_id, 0, 'JavaScript');

                }

            }


        });
        return false;

    }
    else {

        if (functionname_id != null && isserver == '0') {
           
            window.setTimeout(functionname_id, 0, 'JavaScript');

        }

    }


}

var lastExecutor_ProvisionLimit = null;
function HighlightItemsForProvisionLimit(Document_Code) {

    if (lastExecutor_ProvisionLimit != null)
        lastExecutor_ProvisionLimit.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_Check_Provision_Limit', false, { 'Document_Code': Document_Code }, OnSuccessHighlightItemsForProvisionLimit, Onfail);
    lastExecutor_ProvisionLimit = service.get_executor();

}


function OnSuccessHighlightItemsForProvisionLimit(retval) {

    var griditems = document.getElementById("ctl00_MainContent_rgdQuatationInfo");


    if (retval.indexOf(',') > 0) {
        var arrItemRefCode = retval.split(',');

        for (var j = 2; j < griditems.rows.length; j++) {

            var itemrefcode_id = "";
            var lblItemDesc_id = "";


            itemrefcode_id = (j < 10) ? "ctl00_MainContent_rgdQuatationInfo_ctl0" + j.toString() + "_hdfItemRef_Code" : "ctl00_MainContent_rgdQuatationInfo_ctl" + j.toString() + "_hdfItemRef_Code";
            lblItemDesc_id = (j < 10) ? "ctl00_MainContent_rgdQuatationInfo_ctl0" + j.toString() + "_lblItemDesc" : "ctl00_MainContent_rgdQuatationInfo_ctl" + j.toString() + "_lblItemDesc";
            if (arrItemRefCode.indexOf(document.getElementById(itemrefcode_id).value) > -1) {

                document.getElementById(lblItemDesc_id).className = 'NotMatchingProvisionLimit';
                document.getElementById(lblItemDesc_id).style.color = 'white';
            }
        }

        alert('Few items do not comply with provision library . These item are highlighted in red color. ');
    }
}
function ViewReqsnProvisionDetails(queryString) {
    document.getElementById('iframeprvDetails').src = queryString + "&a=" + Date.now().toString();
    showModal('dvProvisionDetails');


}