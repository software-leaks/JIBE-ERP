

function iterateCheckBoxListUserCintrol(ctlidchkSelectAll, ctlidCheckBoxListItems) {
    try {

        var elementRef = document.getElementById(ctlidCheckBoxListItems);
        var chkSelectAll = document.getElementById(ctlidchkSelectAll);
        var checkBoxArray = elementRef.getElementsByTagName('input');



        for (var i = 0; i < checkBoxArray.length; i++) {

            if (chkSelectAll.checked) {

                checkBoxArray[i].checked = true;
            }
            else {

                checkBoxArray[i].checked = false;
            }


        }

        return false;
    }
    catch (e) {

    }
}


function sortcheckboxlistUserControl(check_box_list_client_id) {
    try {

        // get the containing element - should be an HTML table 
        var cbl = $('#' + check_box_list_client_id);
        // check if the jquery element has any items in it 
        if (cbl.length) {
            // get all the table rows, and filter out all  those which 
            // doesn't contain a checked checkbox 
            var cbElements = cbl.find('TR').filter(function (index, element) {
                return $(this).find('input').length;
            });
            // take each table row containing a checked checkbox and place it 
            // at the top of your check-box-list element we called cbl 
            cbElements.each(function () {
                $(this).prependTo(cbl);
            });
        }
    }
    catch (e) {

    }
}


function findcheckBoxListUserControl(ctlidCheckBoxListItems, ctlidsearchtext, e) {
    try {
        //    if (e.keyCode == 13) {

        var elementRef = document.getElementById(ctlidCheckBoxListItems);
        var checkBoxArray = elementRef.getElementsByTagName('input');
        var lblItemTextArray = elementRef.getElementsByTagName('label');

        var searstring = new RegExp(document.getElementById(ctlidsearchtext).value, 'gi');

        for (var i = 0; i < checkBoxArray.length; i++) {

            if (lblItemTextArray[i].innerHTML.match(searstring) != null && $.trim(document.getElementById(ctlidsearchtext).value) != "") {

                lblItemTextArray[i].style.color = 'red';
                lblItemTextArray[i].style.backgroundColor = 'yellow';
            }
            else {
                lblItemTextArray[i].style.color = 'black';
                lblItemTextArray[i].style.backgroundColor = 'white';
            }

        }
        //    }
        return false;
    }
    catch (e) {

    }
}


function cleartextBoxSearch(srcid) {

    document.getElementById(srcid).value = "";
}