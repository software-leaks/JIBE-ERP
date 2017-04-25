var __app_name = location.pathname.split('/')[1];

function ShowCustomFilterUserControl(evt, dvID) {

    try {

        HideCustomFilterUserControl();

        $('#' + dvID).show();

        SetPosition_Relative(evt, dvID);
        debugger;
        window.document.onkeydown = function (e) {
            if (!e)
                e = event;

            if (e.keyCode == 27) {

                HideCustomFilterUserControl();
                HideListBoxItems_dll();
            }
        }

    }
    catch (e) {

    }
}

function HideCustomFilterUserControl() {
try
{
    $('.css-dvfilterlist-jqureyhideshow').hide();
}
catch (e) {

}


}


function checkUcForDateTypeSelectionDateFilter(rbtnSingleID, rbtnDoubleID, listitemID, txttoID) {
    try {

        var rbtnSingleIDChecked = document.getElementById(rbtnSingleID).checked;
        var rbtnDoubleIDChecked = document.getElementById(rbtnDoubleID).checked;

        var list = document.getElementById(listitemID);

        if (rbtnSingleIDChecked)
            document.getElementById(txttoID).style.display = "none";
        else
            document.getElementById(txttoID).style.display = "block";

        for (var i = 0; i < list.options.length; ++i) {

            if (rbtnDoubleIDChecked) {

                if (list.options[i].value != "Between")
                    list.options[i].disabled = "disabled";
                else
                    list.options[i].disabled = "";

            }
            else {

                if (list.options[i].value == "Between")
                    list.options[i].disabled = "disabled";
                else
                    list.options[i].disabled = "";

            }

            if (list.options[i].value == "nofilter")
                list.options[i].disabled = "";
        }

    }
    catch (e) {

    }

}


function clearSelectionOnTextChanged(listitemid, e) {

    try {
        if (e.keyCode != 27 && e.keyCode != 9 && e.keyCode != 16 && e.keyCode != 17 && e.keyCode != 18 && e.keyCode != 19 && e.keyCode != 20 && e.keyCode != 33 && e.keyCode != 34 && e.keyCode != 35 && e.keyCode != 36 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40) {
            var list = document.getElementById(listitemid);
            for (var i = 0; i < list.options.length; ++i) {

                list.options[i].selected = "";

            }
        }

    }
    catch (e) {

    }
}

// will be used on postback
function ShowHideListBoxItems_dll_postback(pnlid, imgid) {
    try {

        var objImgEC = document.getElementById(imgid);
        if (objImgEC != null) {//** Condition is added By Someshwar On 02/03/15. Because Throws Error When Control is Null **//
            var imsrc = objImgEC.src;


            if (imsrc.indexOf('collapse_blue') != -1) {
                objImgEC.src = "/" + __app_name + "/Images/expand_blue.png";
                document.getElementById(pnlid).style.display = 'block';
            }
            else {
                objImgEC.src = "/" + __app_name + "/Images/collapse_blue.png";
                document.getElementById(pnlid).style.display = 'none';
            }
        }
    }
    catch (e) {

    }

}

// will be used on image click
function ShowHideListBoxItems_dll(pnlid, imgid) {
    try {

        var objImgEC = document.getElementById(imgid);
        var imsrc = objImgEC.src;

        HideListBoxItems_dll();

        if (imsrc.indexOf('collapse_blue') != -1) {
            objImgEC.src = "/" + __app_name + "/Images/expand_blue.png";
            document.getElementById(pnlid).style.display = 'block';
            window.document.onkeydown = function (e) {
                if (!e)
                    e = event;

                if (e.keyCode == 27) {

                    HideListBoxItems_dll();
                }
            }


        }
        else {
            objImgEC.src = "/" + __app_name + "/Images/collapse_blue.png";
            document.getElementById(pnlid).style.display = 'none';
        }

    }
    catch (e) {

    }

}


// will be used at textbox search click and if list is collapsed then it will expd
function ExpandOnSearchClick(pnlid, imgid) {
    try {

        HideListBoxItems_dll();

        var objImgEC = document.getElementById(imgid);
        var imsrc = objImgEC.src;

        if (imsrc.indexOf('collapse_blue') != -1) {
            objImgEC.src = "/" + __app_name + "/Images/expand_blue.png";
            document.getElementById(pnlid).style.display = 'block';
        }

    }
    catch (e) {

    }

}



function HideListBoxItems_dll() {
try
{

    $('.pnlListSection-list-ucListBox').hide();
    $('.imgCollapseExpandDDL-css-hide-show').attr("src", "/" + __app_name + "/Images/collapse_blue.png");
}
catch (e) {

}

}