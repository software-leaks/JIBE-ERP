var userSession = "";
var UCID = "";
var lastExecutorMinMaxQty_M = null;
var isloaded = false;
var checkedUserValue = [];
var SelectModeCode = "";

var pager = new Pager('tblmenu', 20);
//pager.init();
//pager.showPageNav('pager', 'pageNavPosition');
//pager.showPage(1);

function FetchData(CUserID, UserCompanyID) {

    if (CUserID != null) {
        userSession = parseInt(CUserID);
    }
    else {
        userSession = 0;
    }

    if (UserCompanyID != null) {
        UCID = parseInt(UserCompanyID);

    }

    dvButton.style.visibility = "hidden";

    BindCompanyName();

    RBCopy();

    Async_CreateTreeStructure();
    DDLUserList();

    BindDDLAppendMode();
    BindDDLCopyMenu();


}


function onchangecompany() {

    BindUserList();
   // BindUserDDL();
}

function BindCompanyName() {



    if (lastExecutorMinMaxQty_M != null)
        lastExecutorMinMaxQty_M.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInfra.asmx', 'GetCompanyList', false, {}, OnGetCompanyListSuccess, OnGetCompanyListFail); //"UserID": userSession 
    lastExecutorMinMaxQty_M = service.get_executor();

}





function OnGetCompanyListSuccess(retVal) {



    var dvCompanyName = document.getElementById("dvlstCompany");

    var dropdown = document.createElement("select");
    debugger;
    dropdown.setAttribute('id', 'lstCompany');
    dropdown.setAttribute('onchange', 'onchangecompany();' );
    dropdown.style.width = "204px";

    var table = document.createElement('table');


    table.innerHTML = retVal;

    var count = table.rows.length;

    var itm = "";
    var value = "";
    var defaultitem = 0;
    var opt = document.createElement('option');

    for (i = 1; i < count; i++) {

        itm = table.rows[i].cells.item(0).innerHTML.trim();
        value = table.rows[i].cells.item(1).innerHTML.trim();
        opt.text = itm;
        opt.value = value;


        dropdown.options.add(new Option(itm, value));
    }

    debugger;
    dvCompanyName.appendChild(dropdown);
    var element = document.getElementById('lstCompany');
    element.value = UCID;

    SearchUser();
    BindUserList();
    
}


function OnGetCompanyListFail(retVal) {


    //    alert('fail' + retVal._message);
}




function RBCopy() {
    // 
    var dvRBCopyFrom = document.getElementById("dvRBCopyFrom");
    var RbtnCopyFromUser = document.createElement("input");
    var labelCopyFromUser = document.createElement("label");
    RbtnCopyFromUser.setAttribute("type", "radio");
    RbtnCopyFromUser.setAttribute("id", "RbtnCopyFromUser");
    RbtnCopyFromUser.setAttribute("name", "RbtnCopy");
    RbtnCopyFromUser.setAttribute("value", 1);
    RbtnCopyFromUser.setAttribute("checked", true);
    RbtnCopyFromUser.setAttribute("onclick", "RbtnUserSelect();");
    labelCopyFromUser.setAttribute("for", RbtnCopyFromUser);
    labelCopyFromUser.setAttribute("onclick", "RbtnUserSelect();");
    labelCopyFromUser.innerHTML = "Copy From User";

    var RbtnCopyFromRole = document.createElement("input");
    var labelCopyFromRole = document.createElement("label");
    RbtnCopyFromRole.setAttribute("type", "radio");
    RbtnCopyFromRole.setAttribute("id", "RbtnCopyFromRole");
    RbtnCopyFromRole.setAttribute("name", "RbtnCopy");
    RbtnCopyFromRole.setAttribute("value", 2);
    RbtnCopyFromRole.setAttribute("onclick", "RbtnRoleSelect();");
    labelCopyFromRole.setAttribute("for", RbtnCopyFromRole);
    labelCopyFromRole.setAttribute("onclick", "RbtnRoleSelect();");
    labelCopyFromRole.innerHTML = "Copy From Role";

    var table = document.createElement("table");
    var row = table.insertRow(0);
    var row1 = table.insertRow(1);

    var cell = row.insertCell(0);
    var cell1 = row.insertCell(1);
    var cell2 = row1.insertCell(0);
    var cell3 = row1.insertCell(1);

    cell.appendChild(RbtnCopyFromUser);
    cell1.appendChild(labelCopyFromUser);
    cell2.appendChild(RbtnCopyFromRole);
    cell3.appendChild(labelCopyFromRole);

    dvRBCopyFrom.appendChild(table);

}



function BindRole() {



    if (lastExecutorMinMaxQty_M != null)
        lastExecutorMinMaxQty_M.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInfra.asmx', 'GetRoleList', false, {}, OnGetRoleListSuccess, OnGetRoleListFail); //"UserID": userSession 
    lastExecutorMinMaxQty_M = service.get_executor();

}


function OnGetRoleListSuccess(retVal) {



    var dvCompanyName = document.getElementById("dvCopyFromRole");

    var dd = document.createElement("select");

    dd.setAttribute('id', 'ddlCopyFromRole');

    dd.style.width = "150px";

    var table = document.createElement('table');


    table.innerHTML = retVal;

    var count = table.rows.length;

    var itm = "";
    var value = "";
    var opt = document.createElement('option');

    for (i = 1; i < count; i++) {

        itm = table.rows[i].cells.item(0).innerHTML.trim();
        value = table.rows[i].cells.item(1).innerHTML.trim();
        opt.text = itm;
        opt.value = value;


        dd.options.add(new Option(itm, value));

    }

    // 
    var children = document.getElementById("dvCopyFromRole").children;

    if (children.length == 0) {
        dvCopyFromRole.appendChild(dd);
    }



}


function OnGetRoleListFail(retVal) {


    //    alert('fail' + retVal._message);
}


function RbtnRoleSelect() {

    document.getElementById("RbtnCopyFromRole").checked = true;
    document.getElementById("dvCopyFromUser").style.display = "none";
    document.getElementById("dvCopyFromRole").style.display = "block";
    DDLUserList();
    document.getElementById("ddlAppendMode").disabled = true;
    document.getElementById("ddlCopyMenu").disabled = true;


}


function RbtnUserSelect() {

    document.getElementById("RbtnCopyFromUser").checked = true;
    document.getElementById("dvCopyFromUser").style.display = "block";
    if (document.getElementById("ddlCopyFromRole") != null) {

        document.getElementById("dvCopyFromRole").style.display = "none";
    }
    DDLUserList();
    document.getElementById("ddlAppendMode").disabled = false;
    document.getElementById("ddlCopyMenu").disabled = false;

}

function DDLUserList() {
    //    
    var userSession = 1;
    var companyid = 0;
    var filtertext = '';

    var ObjArray = [];


    var UserList = new Object();

    UserList["CompanyID"] = companyid;
    UserList["FilterText"] = "";
    UserList["UserID"] = userSession;


    ObjArray[0] = UserList;

    var User_Object = { "UserList": ObjArray };


    var sendurl = "../../JibeWebServiceInfra.asmx/AsyncGetUserList";
    $.ajax({
        type: "POST",
        url: sendurl,
        data: JSON.stringify(User_Object),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        processdata: true,

        success: DDLUserListSuccess

    });

}

function DDLUserListSuccess(data, status, jqXHR) {
    var resultArray = [];
    var len = 0;
    $.each(data, function (key, value) {
        len = value;
    });

    var jsonData = JSON.stringify(len);
    var objData = $.parseJSON(jsonData);
    var source =
            {
                datatype: "json",
                datafields: [
                 { name: 'First_Name' },
                        { name: 'UserName' },
                        { name: 'UserID' }
                ],
                localdata: objData

            };


    var dataAdapter = new $.jqx.dataAdapter(source, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Bad request'); } });

    document.getElementById("ctl00_MainContent_hdnUserListValue").value = "";
    $("#jqxWidget").jqxDropDownList({ checkboxes: true, source: dataAdapter, displayMember: "UserName", valueMember: "UserID", width: 200, height: 20 });
    $("#jqxWidget").jqxDropDownList('checkIndex', -1);

    $("#jqxWidget").on('checkChange', function (event) {
        if (event.args) {
            var item = event.args.item;
            if (item) {


                var valueelement = $("<div></div>");
                valueelement.text("Value: " + item.value);
                var labelelement = $("<div></div>");
                labelelement.text("Label: " + item.label);
                var checkedelement = $("<div></div>");
                checkedelement.text("Checked: " + item.checked);

                $("#selectionlog").children().remove();
                $("#selectionlog").append(labelelement);
                $("#selectionlog").append(valueelement);
                $("#selectionlog").append(checkedelement);

                var items = $("#jqxWidget").jqxDropDownList('getCheckedItems');
                var checkedItems = "";

                var checkedItemsValue = [];
                $.each(items, function (index) {
                    checkedItems += this.label + ", ";

                    checkedItemsValue.push(this.value);
                });
                $("#checkedItemsLog").text(checkedItems);
                checkedUserValue = [];
                document.getElementById("ctl00_MainContent_hdnUserListValue").value = checkedUserValue = checkedItemsValue;
            }
        }
    });


}

function BindDDLAppendMode() {

    var dvAppendMode = document.getElementById("dvAppendMode");

    var dd = document.createElement("select");

    dd.setAttribute('id', 'ddlAppendMode');

    dd.style.width = "120px";


    var itm = "";
    var value = "";
    var opt = document.createElement('option');


    itm = "Remove Existing";
    value = "0";
    opt.text = itm;
    opt.value = value;
    dd.options.add(new Option(itm, value));

    itm = "Retain Existing";
    value = "1";
    opt.text = itm;
    opt.value = value;
    dd.options.add(new Option(itm, value));

    dvAppendMode.appendChild(dd);
}


function BindDDLCopyMenu() {
    var dvAppendMode = document.getElementById("dvAppendMode");

    var dd = document.createElement("select");

    dd.setAttribute('id', 'ddlCopyMenu');

    dd.style.width = "180px";


    var itm = "";
    var value = "";
    var opt = document.createElement('option');


    itm = "All Menu";
    value = "0";
    opt.text = itm;
    opt.value = value;
    dd.options.add(new Option(itm, value));

    itm = "Selected Module/Sub-Module";
    value = "1";
    opt.text = itm;
    opt.value = value;
    dd.options.add(new Option(itm, value));

    dvAppendMode.appendChild(dd);


}

function CopyMenuAccess() {


    var ddlAppendMode = document.getElementById("ddlAppendMode");
    var ddlCopyMenu = document.getElementById("ddlCopyMenu");
    var lstUserList = document.getElementById("ctl00_MainContent_lstUserList");
    var ddlCopyFromUser = document.getElementById("ddlCopyFromUser");
    var iAppendMode = parseInt(ddlAppendMode.options[ddlAppendMode.selectedIndex].value);
    var iCopyMenu = parseInt(ddlCopyMenu.options[ddlCopyMenu.selectedIndex].value);
    var iSessionUserID = userSession;
    var i = 0;
    var index = lstUserList.selectedIndex;
    var ObjArray = [];

    var selectedindex = $("#jqxWidget").jqxDropDownList('selectedIndex');



    if (document.getElementById("RbtnCopyFromUser").checked == true) {

        var iCopyFromUser = parseInt(ddlCopyFromUser.options[ddlCopyFromUser.selectedIndex].value);


        var Selected_Mod_Code = 0;


        if (selectedindex == -1) {
            alert("Please Select Atleast One User ");
            return false;
        }

        else {

            if (iCopyMenu == 1) {





                for (j = 0; j < checkedUserValue.length; j++) {

                    var CopyMenuFromUser = new Object();

                    CopyMenuFromUser["CopyFromUserID"] = iCopyFromUser;
                    CopyMenuFromUser["CopyToUserID"] = checkedUserValue[j];
                    CopyMenuFromUser["AppendMode"] = iAppendMode;
                    CopyMenuFromUser["Selected_Mod_Code"] = SelectModeCode;
                    CopyMenuFromUser["Created_By"] = iSessionUserID;
                    ObjArray[j] = CopyMenuFromUser;
                }
            }

            else {

                SelectModeCode = Selected_Mod_Code = 0;
                for (l = 0; l < checkedUserValue.length; l++) {
                    var CopyMenuFromUser = new Object();
                    CopyMenuFromUser["CopyFromUserID"] = iCopyFromUser;
                    CopyMenuFromUser["CopyToUserID"] = checkedUserValue[l];
                    CopyMenuFromUser["AppendMode"] = iAppendMode;
                    CopyMenuFromUser["Selected_Mod_Code"] = Selected_Mod_Code;
                    CopyMenuFromUser["Created_By"] = iSessionUserID;
                    ObjArray[l] = CopyMenuFromUser;
                }
            }
        }

        var CopyMenu_Object = { "CopyMenuFromUser": ObjArray };

        var sendurl = "../../JibeWebServiceInfra.asmx/AsyncCopyMenuAccessFromUser";

        $.ajax({
            type: "POST",
            url: sendurl,
            data: JSON.stringify(CopyMenu_Object),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processdata: true,
            success: CopyMenuSuccess
        });
    }

    else {

        var iCopyFromRole = parseInt(ddlCopyFromRole.options[ddlCopyFromRole.selectedIndex].value);
        var Selected_Mod_Code = 0;
        var selectedindex = $("#jqxWidget").jqxDropDownList('selectedIndex');

        if (selectedindex == -1) {
            alert("Please Select Atleast One User ");
            return false;
        }

        else {

            if (iCopyMenu == 1) {



                for (var m = 0; m < checkedUserValue.length; m++) {

                    var CopyMenuFromRole = new Object();

                    CopyMenuFromRole["RoleId"] = iCopyFromRole;
                    CopyMenuFromRole["CopyToUserID"] = checkedUserValue[m];
                    CopyMenuFromRole["AppendMode"] = iAppendMode;
                    CopyMenuFromRole["Selected_Mod_Code"] = SelectModeCode;
                    CopyMenuFromRole["Created_By"] = iSessionUserID;
                    ObjArray[m] = CopyMenuFromRole;

                }

            }

            else {

                SelectModeCode = Selected_Mod_Code = 0;
                for (var n = 0; n < checkedUserValue.length; n++) {

                    var CopyMenuFromRole = new Object();

                    CopyMenuFromRole["RoleId"] = iCopyFromRole;
                    CopyMenuFromRole["CopyToUserID"] = checkedUserValue[n];
                    CopyMenuFromRole["AppendMode"] = iAppendMode;
                    CopyMenuFromRole["Selected_Mod_Code"] = Selected_Mod_Code;
                    CopyMenuFromRole["Created_By"] = iSessionUserID;
                    ObjArray[n] = CopyMenuFromRole;

                }

            }
        }

        var CopyMenuRole_Object = { "CopyMenuFromRole": ObjArray };

        var sendurl = "../../JibeWebServiceInfra.asmx/AsyncCopyMenuAccessFromRole";

        $.ajax({
            type: "POST",
            url: sendurl,
            data: JSON.stringify(CopyMenuRole_Object),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processdata: true,

            success: CopyMenuSuccess

        });




    }


}

function CopyMenuSuccess() {

    if (SelectModeCode == 0) {
        alert("All menu access copied");
    }
    else {
        alert("Selected Module/Sub-Module menu access copied");
    }


}




function SearchUser() {
    //  
    var SearchUser = document.getElementById('dvSearchUser');
    var textbox = document.createElement('input');

    textbox.type = "text";
    textbox.setAttribute('id', 'txtSearchUser');
    textbox.style.width = "200px";
    SearchUser.appendChild(textbox);


}


$(document).keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13' && document.getElementById('txtSearchUser') != "") {
        BindUserList();
    }
});


function BindUserList() {

    debugger;

    var lstcompany = document.getElementById('lstCompany');
    var companyid = parseInt(lstcompany.options[lstcompany.selectedIndex].value);
    if (companyid == 0) {
        companyid = parseInt(UCID.toString());
    }
    var filtertext = document.getElementById('txtSearchUser').value;

    if (lastExecutorMinMaxQty_M != null)
        lastExecutorMinMaxQty_M.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInfra.asmx', 'GetUserList', false, { "CompanyID": companyid, "FilterText": filtertext, "UserID": userSession }, OnGetUserListSuccess, OnGetUserListFail); //"UserID": userSession 
    lastExecutorMinMaxQty_M = service.get_executor();

}


function OnGetUserListSuccess(retVal) {
    //  
    var userlist = document.getElementById('ctl00_MainContent_lstUserList');
   
    userlist.innerHTML = "";

    var table = document.createElement('table');

    table.innerHTML = retVal;

    var count = table.rows.length;

    var itm = "";
    var value = "";
    var opt = document.createElement('option');

    for (i = 1; i < count; i++) {

        itm = table.rows[i].cells.item(0).innerHTML.trim();
        value = table.rows[i].cells.item(1).innerHTML.trim();
        opt.text = itm;
        opt.value = value;

      

        userlist.options.add(new Option(itm, value));
       
    }
    BindUserDDL();
}

function OnGetUserListFail(retVal) {

    //    alert('fail' + retVal._message);
}


function BindUserDDL() {


    var lstcompany = document.getElementById('lstCompany');
    var companyid = parseInt(lstcompany.options[lstcompany.selectedIndex].value);
    if (companyid == 0) {
        companyid = parseInt(UCID.toString());
    }
    var filtertext = "";

    if (lastExecutorMinMaxQty_M != null)
        lastExecutorMinMaxQty_M.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInfra.asmx', 'GetUserDDL', false, { "CompanyID": companyid, "FilterText": filtertext, "UserID": userSession }, OnGetUserDDLSuccess, OnGetUserDDLFail); //"UserID": userSession 
    lastExecutorMinMaxQty_M = service.get_executor();

}

function OnGetUserDDLSuccess(retVal) {
    //  
    var dvCopyFromUser = document.getElementById("dvCopyFromUser");
    dvCopyFromUser.innerHTML = "";

    var dropdown = document.createElement("select");

    dropdown.setAttribute('id', 'ddlCopyFromUser');

    dropdown.style.width = "150px";


    var table = document.createElement('table');

    table.innerHTML = retVal;

    var count = table.rows.length;

    var itm = "";
    var value = "";
    var opt = document.createElement('option');

    for (i = 1; i < count; i++) {

        itm = table.rows[i].cells.item(0).innerHTML.trim();
        value = table.rows[i].cells.item(1).innerHTML.trim();
        opt.text = itm;
        opt.value = value;

        dropdown.options.add(new Option(itm, value));

    }

    dvCopyFromUser.appendChild(dropdown);

}

function OnGetUserDDLFail(retVal) {

    //    alert('fail' + retVal._message);
}

var treeselecteditem = 0;
function ListBoxSelection() {

    var select = document.getElementById('ctl00_MainContent_lstUserList');

    len = document.getElementById('ctl00_MainContent_lstUserList').length;

    for (var j = 0; j < len; j++) {
        if (document.getElementById('ctl00_MainContent_lstUserList').options[j].selected) {


            var item = $('#FunctionalTree').jqxTree('getSelectedItem');

            var _id = $("#FunctionalTree").jqxTree('getItem', args.element);
            if (item != null) {
                if (item.selected = true) {
                    select[j].focus();



                    if (_id != null && userSession != "") {

                        Async_CreateTableMenu(_id.id, _id.value);
                    }
                    else {
                        id = "";

                    }
                }
            }
        }
    }

}

function Async_CreateTableMenu(id, modvalue) {

    var modId = parseInt(id);
    var lstUserList = document.getElementById("ctl00_MainContent_lstUserList");
    if (lstUserList.selectedIndex != -1) {
        var selectedValue = lstUserList.options[lstUserList.selectedIndex].value;
    }
    else {
        var selectedValue = "";

    }
    var lstuserid = 0;

    var modvalue = modvalue;

    if (lstUserList.selectedIndex != -1) {

        lstuserid = parseInt(selectedValue);
    }


    if (lastExecutorMinMaxQty_M != null)
        lastExecutorMinMaxQty_M.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInfra.asmx', 'CreateTableMenu', false, { "ListUserID": lstuserid, "ModId": modId }, OnCreateTableMenuSuccess, OnCreateTableMenuFail); //"UserID": userSession 
    lastExecutorMinMaxQty_M = service.get_executor();

}


function OnCreateTableMenuSuccess(retVal) {


    var menuView = document.getElementById('dvMenuGrid');

    menuView.innerHTML = "";
    var BtnResetMenu = document.getElementById('btnResetMenu');

    var BtnSave = document.getElementById('btnSave');

    var dvButton = document.getElementById('dvButton');

    var rows = $(retVal).find('tr').length;
    document.getElementById("ctl00_MainContent_hdnRows").value = "";
    document.getElementById("ctl00_MainContent_hdnRows").value = rows;

    menuView.innerHTML = retVal;
    var tblmenu = document.getElementById('tblmenu');


    if (rows > 1) {


        dvButton.style.visibility = "visible";
        BtnResetMenu.enabled = true;
        BtnSave.enabled = true;


    }
    else {

        dvButton.style.visibility = "hidden";


    }

    var pager = new Pager('tblmenu', 20);
    pager.init();
    pager.showPageNav('pager', 'pageNavPosition');
    pager.showPage(1);



    return false;
}

function OnCreateTableMenuFail(retVal) {

    //    alert(retVal._message);

}


function checkAll(ele) {

    var row = ele.parentNode.parentNode;
    var checkboxes = row.getElementsByTagName('input');

    if (ele.checked) {

        for (var i = 0; i < checkboxes.length; i++) {

            if (checkboxes[i].type == 'checkbox') {

                checkboxes[i].checked = true;

            }




        }
    }
    else {

        for (var i = 0; i < checkboxes.length; i++) {

            console.log(i)
            if (checkboxes[i].type == 'checkbox') {

                checkboxes[i].checked = false;

            }

        }



    }

}

function ResetTableMenu() {
    // 
    var iUserID = 0;

    var lstUserList = document.getElementById('ctl00_MainContent_lstUserList');
    var selected_index = lstUserList.selectedIndex;
    var table = document.getElementById('tblmenu');
    if (selected_index > 0) {
        var iUserID = lstUserList[selected_index].value;
    }
    var checkboxes = table.getElementsByTagName('input');


    if (selected_index != -1) {
        if (lstUserList.options.selected = true) {

            for (var i = 0; i < checkboxes.length; i++) {

                if (checkboxes[i].type == 'checkbox') {

                    checkboxes[i].checked = false;

                }

            }

        }
    }

    else {



    }


    SaveMenuChanges();


    var iSessionUserID = userSession;



    if (iUserID = iSessionUserID) {

        Async_CreateTreeStructure();

    }
}
var intervalID = "";
function SaveMenuChanges() {
    debugger;
    var lstUserList = document.getElementById('ctl00_MainContent_lstUserList');
    var selected_index = lstUserList.selectedIndex;
    if (selected_index != -1) {

        if (lstUserList.options.selected = true) {
            Save();
        }

    }
    else {

        alert('Please Select A User')

    }
}
var lastExecutorMachineUpdateUserMenuAccess = null;
function Save() {
    debugger;
    document.getElementById("progressbar").style.visibility = "visible";
    $("#progressbar").progressbar("value", 0);
    $("#lbldisp").show();
    //call back function    
    intervalID = setInterval(updateProgress, 250);
    document.getElementById("btnSave").disabled = true;
    document.getElementById("btnResetMenu").disabled = true;
    var iMenu = 1;

    var iView = 1;
    var iAdd = 1;
    var iEdit = 1;
    var iDelete = 1;
    var iApprove = 1;
    var iAdmin = 1;
    var iUnverify = 1;
    var iRevoke = 1;
    var iUrgent = 1;
    var iClose = 1;
    var iUnclose = 1;
    var iMenu_Code = 0;
    var iAll = 1;
    var iUserID = 0;
    var inc = 1;
    var iSessionUserID = userSession;

    var lstUserList = document.getElementById('ctl00_MainContent_lstUserList');
    var selected_index = lstUserList.selectedIndex;
    var menutable = document.getElementById('tblmenu');


    var arrMCode = [];
    var arrMenu = [];
    var arrView = [];
    var arrAdd = [];
    var arrEdit = [];
    var arrDelete = [];
    var arrApprove = [];
    var arrAdmin = [];
    var arrUnverify = [];
    var arrRevoke = [];
    var arrUnclose = [];
    var arrUrgent = [];
    var arrClose = [];

    var ObjArray = [];

    debugger;
    if (selected_index != -1) {

        if (lstUserList.options.selected = true) {

            var indx = lstUserList.selectedIndex;

            iUserID = parseInt(lstUserList[indx].value);


            for (var j = 1; j < menutable.rows.length; j++) {



                var lblMenu = "lblMenu_Id_" + inc;
                var chkAll = "chkAll_" + inc;
                var chkAccessView = "chkAccess_View_" + inc;
                var chkAccessAdd = "chkAccess_Add_" + inc;
                var chkAccessEdit = "chkAccess_Edit_" + inc;
                var chkAccessDelete = "chkAccess_Delete_" + inc;
                var chkAccessApprove = "chkAccess_Approve_" + inc;
                var chkAccessAdmin = "chkAccess_Admin_" + inc;
                var chkUnverify = "chkUnverify_" + inc;
                var chkRevoke = "chkRevoke_" + inc;
                var chkUrgent = "chkUrgent_" + inc;
                var chkClose = "chkClose_" + inc;
                var chkUnclose = "chkUnclose_" + inc;

                iMenu_Code = parseInt(document.getElementById(lblMenu).value);
                iAll = (document.getElementById(chkAll).checked == true) ? 1 : 0;

                iMenu = (document.getElementById(chkAccessView).checked == true) ? 1 : 0;
                iView = (document.getElementById(chkAccessView).checked == true) ? 1 : 0;
                iAdd = (document.getElementById(chkAccessAdd).checked == true) ? 1 : 0;
                iEdit = (document.getElementById(chkAccessEdit).checked == true) ? 1 : 0;
                iDelete = (document.getElementById(chkAccessDelete).checked == true) ? 1 : 0;
                iApprove = (document.getElementById(chkAccessApprove).checked == true) ? 1 : 0;
                iAdmin = (document.getElementById(chkAccessAdmin).checked == true) ? 1 : 0;
                iUnverify = (document.getElementById(chkUnverify).checked == true) ? 1 : 0;
                iRevoke = (document.getElementById(chkRevoke).checked == true) ? 1 : 0;
                iUrgent = (document.getElementById(chkUrgent).checked == true) ? 1 : 0;
                iClose = (document.getElementById(chkClose).checked == true) ? 1 : 0;
                iUnclose = (document.getElementById(chkUnclose).checked == true) ? 1 : 0;

                if (iMenu_Code != 0) {

                    var UserMenu = new Object();

                    UserMenu["UID"] = iUserID;
                    UserMenu["MCode"] = iMenu_Code;
                    UserMenu["Menu"] = iMenu;
                    UserMenu["View"] = iView;
                    UserMenu["Add"] = iAdd;
                    UserMenu["Edit"] = iEdit;
                    UserMenu["Delete"] = iDelete;
                    UserMenu["Approve"] = iApprove;
                    UserMenu["Admin"] = iAdmin;
                    UserMenu["Unverify"] = iUnverify;
                    UserMenu["Revoke"] = iRevoke;
                    UserMenu["Unclose"] = iUnclose;
                    UserMenu["Urgent"] = iUrgent;
                    UserMenu["Close"] = iClose;
                    UserMenu["SessionUserID"] = iSessionUserID;

                }



                inc++;
                ObjArray[j] = UserMenu;
            }



            var Menu_Object = { "UserMenu": ObjArray };

            var sendurl = "../../JibeWebServiceInfra.asmx/UpdateUserMenu";



            $.ajax({

                url: sendurl,
                type: "POST",
                data: JSON.stringify(Menu_Object),

                contentType: "application/json; charsert=utf-8",
                dataType: "json",
                processdata: true,
                success: OnUpdateUserMenuSuccess,
                error: OnUpdateUserMenuFail
            });
            return false;

        }



    }


}



function OnUpdateUserMenuSuccess() {
    debugger;

    $("#progressbar").progressbar("value", 100);
    $("#lblStatus").hide();
    $("#lbldisp").hide();
    $("#result").text();
    clearInterval(intervalID);

    alert('Menu Has Been Assigned Successfully');
    document.getElementById("progressbar").style.visibility = "hidden";
    document.getElementById("btnSave").disabled = false;
    document.getElementById("btnResetMenu").disabled = false;
    Async_CreateTreeStructureResult();

    $("#FunctionalTree").jqxTree('focus');
    $('#FunctionalTree').jqxTree({ 'source': treedata, height: '720px', width: '350px' });

}



function OnUpdateUserMenuFail() {

    //    alert('Update UserMenu fail');

}






function Async_CreateTreeStructureResult() {
    //  

    var sendurl = "../../JibeWebServiceInfra.asmx/CreateTreeStructure";

    $.ajax({

        url: sendurl,
        type: "GET",
        data: { "UserID": userSession },
        contentType: "application/json; charsert=utf-8",
        dataType: "json",
        processdata: true,
        success: OnCreateTreeSuccess,
        error: OnCreateTreeFail
    });

}

function Async_CreateTreeStructure() {


    var sendurl = "../../JibeWebServiceInfra.asmx/CreateTreeStructure";

    $.ajax({

        url: sendurl,
        type: "GET",
        data: { "UserID": userSession },
        contentType: "application/json; charsert=utf-8",
        dataType: "json",
        processdata: true,
        success: OnCreateTreeSuccess,
        error: OnCreateTreeFail
    });

}


var treedata = '';
function OnCreateTreeSuccess(data, status, jqXHR) {

    if (isloaded == true) {
        $('#FunctionalTree').remove();
        $('#maintree ').prepend($('<div id=FunctionalTree></div>'));

        isloaded = false;
    }

    var len = 0;
    $.each(data, function (key, value) {
        len = value;
    });

    var jsonData = JSON.stringify(len);
    var objData = $.parseJSON(jsonData);


    var source =
                {
                    datatype: "json",
                    datafields: [
                        { name: 'id' },
                        { name: 'icon' },
                        { name: 'parent' },
                        { name: 'text' },
                        { name: 'value' }
                    ],
                    id: 'id',
                    localdata: objData
                };


    var dataAdapter = new $.jqx.dataAdapter(source);
    dataAdapter.dataBind();
    var records = dataAdapter.getRecordsHierarchy('id', 'parent', 'items', [{ name: 'value', map: 'label' }, { name: 'text', map: 'label'}]);

    treedata = records;
    $('#FunctionalTree').jqxTree({ 'source': records, height: '720px', width: '350px' });
    if (treeselecteditem != '') {
        $('#FunctionalTree').jqxTree({ 'source': records, height: '595px', width: '350px' });
        $('#FunctionalTree').jqxTree('selectItem', $("#FunctionalTree").find('#' + treeselecteditem)[0]);

    }
    $("#FunctionalTree").bind('select', function (e) {

        var args = e.args;
        _id = $("#FunctionalTree").jqxTree('getItem', args.element);
        if (_id != null && userSession != "") {


            var item = $('#FunctionalTree').jqxTree('getSelectedItem');
            treeselecteditem = item.element.id;

            SelectModeCode = _id.value.split('_')[1];
            Async_CreateTableMenu(_id.id, _id.value);

        }
        else {
            id = "";

        }
    });

    isloaded = true;

    setTimeout(function () {
        BindRole();
    }, 1000);


}


function OnCreateTreeFail() {

}

function updateProgress() {
    var value = $("#progressbar").progressbar("option", "value");
    if (value < 100) {
        $("#progressbar").progressbar("value", value + 1);
        $("#lblStatus").text((value + 1).toString() + "%");
    }
}           