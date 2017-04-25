var __app_name = location.pathname.split('/')[1];

function load_port_list(lstboxid, searchParamTextBoxID, hdf_wm, hdf_extra_filter) {



    var lBox = $('select[id$=' + lstboxid + ']');
    lBox.empty();
    var searchParam = document.getElementById(searchParamTextBoxID).value;
    var _extra_filter = document.getElementById(hdf_extra_filter).value;

    if (searchParam == "Search")
        searchParam = "";

    $.ajax({
        type: "POST",
        url: hdf_wm,
        data: "{searchText:\"" + searchParam + "\",selectedValue:\"0\",extraSearch:\"" + _extra_filter + "\"}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            var portlist = msg.d;
            if (portlist.length > 0) {
                var listItems = [];
                for (var key in portlist) {
                    listItems.push('<option  value="' +
                                portlist[key].DataValue + '">' + portlist[key].DataText
                                + '</option>');
                }
                $(lBox).append(listItems.join(''));
            }
            else {

            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {


        }
    });
}


function load_selected_Text(lstboxid, searchParamTextBoxID, hdf_wm, selected_value, hdf_selected_text_id, txtSelectedPortNameid, hdf_extra_filter) {

    var lBox = $('select[id$=' + lstboxid + ']');
    lBox.empty();
    var searchParam = document.getElementById(searchParamTextBoxID).value;
    var _extra_filter = document.getElementById(hdf_extra_filter).value;
    if (searchParam == "Search")
        searchParam = "";

    $.ajax({
        type: "POST",
        url: hdf_wm,
        data: "{searchText:\"" + searchParam + "\",selectedValue:\"" + selected_value + "\",extraSearch:\"" + _extra_filter + "\"}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            var portlist = msg.d;
            if (portlist.length > 0) {
                var listItems = [];
                for (var key in portlist) {

                    document.getElementById(hdf_selected_text_id).value = portlist[key].DataText;
                    document.getElementById(txtSelectedPortNameid).value = portlist[key].DataText;
                }

            }
            else {

            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {


        }
    });
}


function setSelectedPortlistItems(port_id_hiddenfield_id, hdf_selected_text_id, txtSelectedPortNameid, ListBoxPortlistid) {

    var elementRef = document.getElementById(ListBoxPortlistid)

    var checkBoxArray = elementRef.getElementsByTagName('option');


    for (var i = 0; i < checkBoxArray.length; i++) {

        if (checkBoxArray[i].selected == true) {

            document.getElementById(port_id_hiddenfield_id).value = checkBoxArray[i].value;
            document.getElementById(txtSelectedPortNameid).value = checkBoxArray[i].innerHTML;
            document.getElementById(hdf_selected_text_id).value = checkBoxArray[i].innerHTML;
        }


    }
    HideListBoxItems_dll();

}

function ShowHideListBoxItems_Async_dll(pnlid, imgid, lstboxid, searchParamTextBoxID, web_method, hdf_extra_filter) {

    var objImgEC = document.getElementById(imgid);
    var imsrc = objImgEC.src;

    HideListBoxItems_dll();

    if (imsrc.indexOf('collapse_blue') != -1) {
        objImgEC.src = "/" + __app_name + "/Images/expand_blue.png";
        document.getElementById(pnlid).style.display = 'block';

        var elementRef = document.getElementById(lstboxid)
        var checkBoxArray = elementRef.getElementsByTagName('option');

        if (checkBoxArray.length < 1) {
            load_port_list(lstboxid, searchParamTextBoxID, web_method, hdf_extra_filter);
        }

    }
    else {
        objImgEC.src = "/" + __app_name + "/Images/collapse_blue.png";
        document.getElementById(pnlid).style.display = 'none';
    }


}


// will be used on postback
function ShowHideListBoxItems_Async_dll_postback(pnlid, imgid) {
    try {
        var objImgEC = document.getElementById(imgid);
        var imsrc = objImgEC.src;


        if (imsrc.indexOf('collapse_blue') != -1) {
            objImgEC.src = "/" + __app_name + "/Images/expand_blue.png";
            document.getElementById(pnlid).style.display = 'block';
        }
        else {
            objImgEC.src = "/" + __app_name + "/Images/collapse_blue.png";
            document.getElementById(pnlid).style.display = 'none';
        }
    } catch (ex) {
    }

}


function LoadPortList_txtsearch_Press_enter(e, lstboxid, searchParamTextBoxID, web_method, hdf_extra_filter) {

    if (e.which == 13 || document.getElementById(searchParamTextBoxID).value == "") {
        load_port_list(lstboxid, searchParamTextBoxID, web_method, hdf_extra_filter)
    }


}



