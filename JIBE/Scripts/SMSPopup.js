var ajxStatusId;
var ajxReadyText;
var ajxTargetId;
var aW;
var aH;

function fenOnClose(xf) {
    document.getElementById('ucTitle_cmdHiddenSubmit').click();
    return true;
}

/* needed for filter criteria clear */
var strFilterWindowTitle;
var strFilterWindowURL;
var strFilterWindowSize;
/* needed for filter criteria clear */

function AjxGet(url, targetId, async) {
    ajxTargetId = targetId;
    if (async == null) { async = true; }
    getDoc(url, FnWriteHTMLToControl, ajxTargetId, ajxStatusId, async, '');
}

function AjxGet_WithPostAjaxCall(url, targetId, async, fnPostAjax, fnPostAjaxParams) {
    ajxTargetId = targetId;
    if (async == null) { async = true; }
    getDoc_WithPostAjaxCall(url, FnWriteHTMLToControl_WithPostAjaxCall, ajxTargetId, ajxStatusId, async, fnPostAjax, fnPostAjaxParams, '');
}

function AjxPost(postMsg, url, targetId, async) {
    ajxTargetId = targetId;
    if (async == null) { async = true; }
    return getDoc(url, FnWriteHTMLToControl, ajxTargetId, ajxStatusId, async, postMsg);
}

function AjxPostWithPostAjaxCall(postMsg, url, targetId, async, fnPostAjax, fnPostAjaxParams) {
    ajxTargetId = targetId;
    if (async == null) { async = true; }
    return getDoc_WithPostAjaxCall(url, FnWriteHTMLToControl_WithPostAjaxCall, ajxTargetId, ajxStatusId, async, fnPostAjax, fnPostAjaxParams, postMsg);
}

function getDoc(url, doFunc, targetId, statusId, async, postMsg) {

    var xmlhttp;
    var msg = null;
    var ajaxmethodtype = "GET";
    var retVal = null;
    if (postMsg != null && postMsg.length > 0) {
        msg = postMsg;
        ajaxmethodtype = "POST";
    }
    if (document.getElementById(targetId) == null && ajaxmethodtype == "GET") {
        ServerTrappableJSError("The argument 'targetId' with value '" + targetId + "' supplied to getDoc is not valid for ajax 'GET'");
        return;
    }
    try {
        xmlhttp = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHTTP");
        xmlhttp.onreadystatechange = function () {
            if (xmlhttp.readyState == 4) {
                if (xmlhttp.status == 200) {
                    doFunc(xmlhttp, targetId, statusId);
                    return xmlhttp.responseText
                }
                else if (xmlhttp.status == 404) {
                    alert('Requested page not found(Error: 404)');
                    doFunc(xmlhttp, targetId, statusId);
                    return 'ERROR 404';
                }
                else if (xmlhttp.status == 500) {
                    alert('Internal server error(500) occured. If this problem persists, contact administrator');
                    doFunc(xmlhttp, targetId, statusId);
                    return 'ERROR 500';
                }
                else {
                    return 'HTML STATUS: ' + xmlhttp.status;
                    //alert('HTTP status: ' + xmlhttp.status + ' occured.');
                }
            }
            else {
                doFunc(xmlhttp, targetId, statusId);
            }
        }
        if (async == null) { async = true; }
        xmlhttp.open(ajaxmethodtype, url, async);
        xmlhttp.send(msg);

        if (async == false) {
            if (document.getElementById(targetId) != null)
                FnWriteHTMLToControl(xmlhttp, targetId, statusId);
            return xmlhttp.responseText;
        }
    }
    catch (e) {
        alert('getDoc: ' + e);
        return 'ERROR: ' + e.toString();
    }
}

function getDoc_WithPostAjaxCall(url, doFunc, targetId, statusId, async, fnPostAjax, fnPostAjaxParams, postMsg) {
    var xmlhttp;
    var msg = null;
    var ajaxmethodtype = "GET";
    var retVal = null;
    if (postMsg != null && postMsg.length > 0) {
        msg = postMsg;
        ajaxmethodtype = "POST";
    }
    if (document.getElementById(targetId) == null && ajaxmethodtype == "GET") {
        ServerTrappableJSError("The argument 'targetId' with value '" + targetId + "' supplied to getDoc is not valid for ajax 'GET'");
        return;
    }
    try {
        xmlhttp = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHTTP");
        xmlhttp.onreadystatechange = function () {
            if (xmlhttp.readyState == 4) {
                if (xmlhttp.status == 200) {
                    doFunc(xmlhttp, targetId, statusId, fnPostAjax, fnPostAjaxParams);
                    return xmlhttp.responseText;
                }
                else if (xmlhttp.status == 404) {
                    alert('Requested page not found(Error: 404)');
                    doFunc(xmlhttp, targetId, statusId);
                    return 'ERROR: 404';
                }
                else if (xmlhttp.status == 500) {
                    alert('Internal server error(500) occured. If this problem persists, contact administrator');
                    doFunc(xmlhttp, targetId, statusId);
                    return 'ERROR: 500';
                }
                else {
                    //alert('HTTP status: ' + xmlhttp.status + ' occured.');
                    return 'STATUS: ' + xmlhttp.status;
                }
            }
            else {
                doFunc(xmlhttp, targetId, statusId, fnPostAjax, fnPostAjaxParams);
            }
        }
        if (async == null) { async = true; }
        xmlhttp.open(ajaxmethodtype, url, async);
        xmlhttp.send(msg);

        if (async == false) {
            FnWriteHTMLToControl_WithPostAjaxCall(xmlhttp, targetId, statusId, fnPostAjax, fnPostAjaxParams);
            return xmlhttp.responseText;
        }
    }
    catch (e) {
        HandleException(e, 'getDoc_WithPostAjaxCall(url, doFunc, targetId, statusId, async, fnPostAjax, fnPostAjaxParams)');
        return 'ERROR: ' + e.toString();
    }
}

function FnWriteHTMLToControl(req, targetId, statusId) {
    try {
        if (req.readyState == 4 && document.getElementById(targetId) != null) {
            document.getElementById(targetId).innerHTML = req.responseText;
        }
        else if (req.readyState == 1 && (document.getElementById(targetId) && document.getElementById(targetId))) {
            document.getElementById(targetId).innerHTML = ajxLoadingText;
        }
        else {
            try {
                if (document.getElementById(targetId) != null) {
                    document.getElementById(targetId).innerHTML = req.responseText;
                }
            }
            catch (e) {
                /*do nothing.*/
            }
        }
    }
    catch (e) {
        alert('FnWriteHTMLToControl: ' + e);
    }
}

function FnWriteHTMLToControl_WithPostAjaxCall(req, targetId, statusId, fnPostAjax, fnPostAjaxParams) {
    try {
        if (req.readyState == 4) {
            document.getElementById(targetId).innerHTML = req.responseText;
            if (fnPostAjax) {
                eval('fnPostAjax(fnPostAjaxParams)');
            }
        }
        else if (req.readyState == 1 && (document.getElementById(targetId))) {
            document.getElementById(targetId).innerHTML = ajxLoadingText;
        }
        else {
            try {
                document.getElementById(targetId).innerHTML = req.responseText;
            }
            catch (e) {
                /*do nothing.*/
            }
        }
    }
    catch (e) {
        HandleException(e, 'FnWriteHTMLToControl_WithPostAjaxCall(req, targetId, statusId, fnPostAjax, fnPostAjaxParams)');
    }
}

function FnWriteHTMLToFrame(req, targetId, statusId) {
    try {
        if (req.readyState == 4) {
            document.getElementById(targetId).contentDocument.getElementById("IFRAME_DIV").innerHTML = req.responseText;
            document.getElementById(statusId).innerHTML = ajxReadyText;
        }
        else if (req.readyState == 1 && (document.getElementById(targetId))) {
            document.getElementById(targetId).innerHTML = ajxLoadingText;
        }
        else {
            try {
                document.getElementById(targetId).innerHTML = req.responseText;
            }
            catch (e) {
                /*do nothing.*/
            }
        }
    }
    catch (e) {
        HandleException(e, 'FnWriteHTMLToFrame(req, targetId, statusId)');
    }
}

function PopulateControl(strURL, strFunctionName, strParams, strTarget) {
    AjxGet(strURL + "?f=" + strFunctionName + "|params=" + strParams + ',' + strTarget, strTarget, false);
}


/********************************** AJAX ROUTINES END ******************************/

/********************************** POPUP ROUTINES START ******************************/
function OpenAlertWindow(strTitle, strURL, winSize, name) {
    if (name == null) { name = 'divAlertFilterIFRAME'; }
    OpenPopupWindow(name, strTitle, strURL, "alert", winSize);
}

function CloseAlertWindow() {
    if (xFenster.instances.divAlertFilterIFRAME) {
        xFenster.instances.divAlertFilterIFRAME.hide();
        //xFenster.instances.divAlertFilterIFRAME.destroy();
    }
}

function OpenDatePickerWithDate(strTitle, strURL, parentWindowObj, x, y) {
    var selecteddate = document.getElementById(parentWindowObj).value;
    strURL = strURL + "&selecteddate=" + selecteddate;
    OpenPopupWindow("divDatePickerIFRAME", strTitle, strURL, "datepicker", "datepicker", '', x, y);
}


function OpenDatePicker(strTitle, strURL, parentWindowObj) {
    OpenPopupWindow("divDatePickerIFRAME", strTitle, strURL, "datepicker", "datepicker");
}

function OpenGeneralWindow(strTitle, strURL, winSize, name, bDiv, posx, posy, pW, pH) {
    if (pW) { aW = pW; winSize = "specific"; }
    if (pH) { aH = pH; winSize = "specific"; }
    if (name == null) { name = 'divGeneralFilterIFRAME'; }
    OpenPopupWindow(name, strTitle, strURL, "general", winSize, bDiv, posx, posy);
}

function OpenDashboard(name, strTitle, strURL, posx, posy, pW, pH, bDiv) {
    aW = pW;
    aH = pH;
    OpenPopupWindow(name, strTitle, strURL, "dashboard", "specific", bDiv, posx, posy);
}

function CloseDatePicker(name) {
    if (xFenster.instances.divDatePickerIFRAME) {
        xFenster.instances.divDatePickerIFRAME.hide();
    }
    if (eval("xFenster.instances." + name)) {
        eval("xFenster.instances." + name + ".hide()");
    }
}
function ReloadParent_ByButtonID() {
   
    var dvOverlay = document.getElementById('overlay');
    if (dvOverlay != null)
        dvOverlay.style.display = 'none';

    var hdfParentCtlBtnID = document.getElementById("hdfParentCtlBtnID").value;

    if (hdfParentCtlBtnID != "null") {
        var postBackstr = "__doPostBack('" + hdfParentCtlBtnID + "','" + hdfParentCtlBtnID + "_Click')";
        window.setTimeout(postBackstr, 0, 'JavaScript');
    }

    var closefun = "CloseCodeHelpWindow('" + document.getElementById('hdfPOP__ID').value + "')";
    window.setTimeout(closefun, 500, 'JavaScript');



    return true;

}

function fnReloadParent(name, keeppopupopen) {

    if (keeppopupopen == null) {
        if (name == null)
            CloseWindow('Filter');
        else
            CloseWindow(name);
    }

    var btnHiddenSubmit = parent.document.getElementById('btnHiddenSubmit');

    if (btnHiddenSubmit == null) {
        //when the popup is opening from the Master Page Form
        try {
            parent.document.getElementById('MainContent_btnHiddenSubmit').click();
        } catch (e) {
            parent.document.getElementById('ctl00_MainContent_btnHiddenSubmit').click();
        }
    }
    else {
        //when the popup is opening from Simple Form
        parent.document.getElementById('btnHiddenSubmit').click();
    }
}


function closeMoreInformation() {
   

    CloseCodeHelpWindow('MoreInfo');
}



function CloseCodeHelpWindow(name) {
   
    var dvOverlay = document.getElementById('overlay');
    if (dvOverlay != null)
        dvOverlay.style.display = 'none';

    if (name == null) /*(GetBrowser() == 'IE6')*/
    {
        if (xFenster.instances.divCodeHelpIFRAME) {
            xFenster.instances.divCodeHelpIFRAME.hide();
            xFenster.instances.divCodeHelpIFRAME.destroy();
        }
    }
    else {
        if (eval("xFenster.instances." + name)) {
            eval("xFenster.instances." + name + ".hide()");
            eval("xFenster.instances." + name + ".destroy()");
        }
    }
}


function CloseWindow(name) {
  
    var dvOverlay = document.getElementById('overlay');
    if (dvOverlay != null)
        dvOverlay.style.display = 'none';

    parent.CloseCodeHelpWindow(name);
}


function ___fnReloadList(name, refreshiframe, keeppopupopen) {
    var o;
    if (parent.document.getElementById('filterandsearch') != null)
        o = parent.document.getElementById('filterandsearch');
    if (parent.document.getElementById('fraPhoenixApplication') != null)
        o = parent.document.getElementById('fraPhoenixApplication').contentDocument.getElementById('filterandsearch');

    if (keeppopupopen == null) {
        if (name == null)
            CloseWindow('Filter');
        else
            CloseWindow(name);
    }

    if (o.contentWindow.document.getElementById(refreshiframe) == null)
        refreshiframe = null;

    if (refreshiframe == null)
        o.contentDocument.getElementById('cmdHiddenSubmit').click();
    else
        o.contentWindow.document.getElementById(refreshiframe).contentDocument.getElementById('cmdHiddenSubmit').click();
}

function Showoverlay() {
    var dvOverlay = document.getElementById('overlay');
    if (dvOverlay == null) {
        dvOverlay = document.createElement('div');
        dvOverlay.id = 'overlay';
        dvOverlay.style.zIndex = "999";
        dvOverlay.style.background = 'black';
        dvOverlay.style.opacity = '0.5';
        dvOverlay.style.filter='alpha(opacity=50)';
      
        dvOverlay.style.width = '100%';
//        dvOverlay.style.height = '100%';
        dvOverlay.style.position = 'absolute';
        dvOverlay.style.top = '0px';
        dvOverlay.style.left = '0px';
        var body = document.body,
             html = document.documentElement;

        var height = Math.max(body.scrollHeight, body.offsetHeight,
                       html.clientHeight, html.scrollHeight, html.offsetHeight);
        dvOverlay.style.height = height + 'px';
        document.body.appendChild(dvOverlay);
    }
   
    dvOverlay.style.display = 'block';

}

function OpenPopupWindow(name, strTitle, strURL, type, ph, pw, posx, posy, IsMinIcon, IsMaxIcon, IsCloseIcon, callback) /*type: alert, filter, codehelp,datepicker*/
{

   




    CloseIcon = "xfCIco_D"; MinIcon = "xafNIco_D"; MaxIcon = "xafMIco_D";

    if ((IsMinIcon == true) || (IsMinIcon == null)) {
        MinIcon = "xafNIco";
    }

    if ((IsMaxIcon == true) || (IsMaxIcon == null)) {
        MaxIcon = "xafMIco";
    }

    if ((IsCloseIcon == true) || (IsCloseIcon == null)) {
        CloseIcon = "xfCIco";
    }

    var x = posx == null ? 240 : posx;
    var y = posy == null ? 200 : posy;
    var w = pw == null ? 960 : pw;
    var h = ph == null ? 480 : ph;

    var cor = getCenterCor(w, h);
    y = (posy != null) ? posy : cor.y / 2;
    x = (posx != null) ? posx : cor.x;


    //Ramesh
    if (type == "popup") {

        CloseCodeHelpWindow(name);
        new xFenster(name, strTitle, strURL, x, y, w, h,
				150, null, 1, 1, 1,
				true, true, true, true, true, false, false,
				null, null, null, null, null, callback, null, null,
				'xfCon', 'xfClient', 'xfTBar', 'xfTBarF', 'xfSBar', 'xfSBarF',
				'xfRIco', MinIcon, MaxIcon, 'xfOIco', CloseIcon,
				'Resize', 'Minimize', 'Maximize', 'Restore', 'Close');


    }
    else if (type == "filter") {
        strFilterWindowTitle = strTitle;
        strFilterWindowURL = strURL;
        strFilterWindowSize = predefinedsize;
        if (GetBrowser() == 'IE6') {
            CloseFilterWindow(name);
            new xFenster(name, XF_Icon('search.gif') + strTitle, strURL, x, y, w, h,
					150, null, 1, 1, 1,
					true, true, true, true, true, false, false,
					null, null, null, null, null, null, null, fensterresize,
					'xafCon', 'xafClient', 'xafTBar', 'xafTBarF', 'xafSBar', 'xafSBarF',
					'xafRIco', 'xafNIco', 'xafMIco', 'xafOIco', 'xafCIco',
					'Resize', 'Minimize', 'Maximize', 'Restore', 'Close'
				);
        }
        else {
            CloseFilterWindow(name);
            if (!(document.getElementById(name))) /*create <div id="divFilter" if not there*/
            {
                var divElement = document.createElement("div");
                divElement.setAttribute("id", name);
                document.body.appendChild(divElement);
                document.getElementById(name).cssText = 'overflow:auto';
            }
            AjxGet(strURL, name);
            new xFenster(name, XF_Icon('search.gif') + strTitle, null, x, y, w, h,
				150, null, 1, 1, 1,
				true, true, true, true, true, false, false,
				null, null, null, null, null, null, null, null,
				'xfCon', 'xfClient', 'xfTBar', 'xfTBarF', 'xfSBar', 'xfSBarF',
				'xfRIco', 'xfNIco', 'xfMIco', 'xfOIco', 'xfCIco',
				'Resize', 'Minimize', 'Maximize', 'Restore', 'Close'
			);
            if (eval("xFenster.instances." + name))
                fensterresize(eval("xFenster.instances." + name));
        }
    }

    Showoverlay();
}




function OpenPopupWindowBtnID(name, strTitle, strURL, type, ph, pw, posx, posy, IsMinIcon, IsMaxIcon, IsCloseIcon, callback, ParentBtnCltID) /*type: alert, filter, codehelp,datepicker*/
{
    
    var hdfParentCtlBtnID = document.getElementById("hdfParentCtlBtnID");
    ParentBtnCltID = ParentBtnCltID == null ? null : ParentBtnCltID.replace(/_/gi, '$');

    if (hdfParentCtlBtnID == null) {
        var hdfinput = document.createElement("input");
        hdfinput.setAttribute("type", "hidden");
        hdfinput.setAttribute("name", "hdfParentCtlBtnIDname");
        hdfinput.setAttribute("id", "hdfParentCtlBtnID");
        hdfinput.setAttribute("value", ParentBtnCltID);
        document.body.appendChild(hdfinput);
    }
    else {

        hdfParentCtlBtnID.value = ParentBtnCltID;

    }


    var hdfPOP__ID = document.getElementById("hdfPOP__ID");

    if (hdfPOP__ID == null) {
        var hdfPOP__IDinput = document.createElement("input");
        hdfPOP__IDinput.setAttribute("type", "hidden");
        hdfPOP__IDinput.setAttribute("name", "hdfPOP__IDname");
        hdfPOP__IDinput.setAttribute("id", "hdfPOP__ID");
        hdfPOP__IDinput.setAttribute("value", name);
        document.body.appendChild(hdfPOP__IDinput);
    }
    else {

        hdfPOP__ID.value = name;

    }



    CloseIcon = "xfCIco_D"; MinIcon = "xafNIco_D"; MaxIcon = "xafMIco_D";

    if ((IsMinIcon == true) || (IsMinIcon == null)) {
        MinIcon = "xafNIco";
    }

    if ((IsMaxIcon == true) || (IsMaxIcon == null)) {
        MaxIcon = "xafMIco";
    }

    if ((IsCloseIcon == true) || (IsCloseIcon == null)) {
        CloseIcon = "xfCIco";
    }

    var x = posx == null ? 240 : posx;
    var y = posy == null ? 200 : posy;
    var w = pw == null ? 960 : pw;
    var h = ph == null ? 480 : ph;

    var cor = getCenterCor(w, h);
    y = (posy != null) ? posy : cor.y;
    x = (posx != null) ? posx : cor.x;


    //Ramesh
    if (type == "popup") {

        CloseCodeHelpWindow(name);
        new xFenster(name, strTitle, strURL, x, y, w, h,
				150, null, 1, 1, 1,
				true, true, true, true, true, false, false,
				null, null, null, null, null, callback, null, null,
				'xfCon', 'xfClient', 'xfTBar', 'xfTBarF', 'xfSBar', 'xfSBarF',
				'xfRIco', MinIcon, MaxIcon, 'xfOIco', CloseIcon,
				'Resize', 'Minimize', 'Maximize', 'Restore', 'Close');


    }
    else if (type == "filter") {
        strFilterWindowTitle = strTitle;
        strFilterWindowURL = strURL;
        strFilterWindowSize = predefinedsize;
        if (GetBrowser() == 'IE6') {
            CloseFilterWindow(name);
            new xFenster(name, XF_Icon('search.gif') + strTitle, strURL, x, y, w, h,
					150, null, 1, 1, 1,
					true, true, true, true, true, false, false,
					null, null, null, null, null, null, null, fensterresize,
					'xafCon', 'xafClient', 'xafTBar', 'xafTBarF', 'xafSBar', 'xafSBarF',
					'xafRIco', 'xafNIco', 'xafMIco', 'xafOIco', 'xafCIco',
					'Resize', 'Minimize', 'Maximize', 'Restore', 'Close'
				);
        }
        else {
            CloseFilterWindow(name);
            if (!(document.getElementById(name))) /*create <div id="divFilter" if not there*/
            {
                var divElement = document.createElement("div");
                divElement.setAttribute("id", name);
                document.body.appendChild(divElement);
                document.getElementById(name).cssText = 'overflow:auto';
            }
            AjxGet(strURL, name);
            new xFenster(name, XF_Icon('search.gif') + strTitle, null, x, y, w, h,
				150, null, 1, 1, 1,
				true, true, true, true, true, false, false,
				null, null, null, null, null, null, null, null,
				'xfCon', 'xfClient', 'xfTBar', 'xfTBarF', 'xfSBar', 'xfSBarF',
				'xfRIco', 'xfNIco', 'xfMIco', 'xfOIco', 'xfCIco',
				'Resize', 'Minimize', 'Maximize', 'Restore', 'Close'
			);
            if (eval("xFenster.instances." + name))
                fensterresize(eval("xFenster.instances." + name));
        }
    }

    Showoverlay();
}


function OpenFilterWindow(strTitle, strURL, winSize, name, pW, pH) {
    if (pW) { aW = pW; winSize = "specific"; }
    if (pH) { aH = pH; winSize = "specific"; }
    var strFilterDiv;
    if (name == null) {
        if (GetBrowser() == "IE6") {
            name = 'divFilterIFRAME';
        }
        else {
            name = 'divFilter';
        }
    }
    OpenPopupWindow(name, strTitle, strURL, "filter", winSize);
}

function XF_Icon(fileName) {
    return '<img valign="top" style="padding-bottom:2px;" border="0" height="14" width="14" src="/Images/' + fileName + '" />&nbsp;'
}




/*IF (TRUE) STATEMENTS IN THIS FUNCTION ENFORCES OPERATION OF CODEHELP TO BE IN IFRAME*/
function OpenCodeHelpWindow(strTitle, strURL, strParentPageURL, strParentControlName, winSize, name) {
    strURL = strURL + "&parentpage=" + strParentPageURL;
    if (strParentControlName) {
        strURL = strURL + "&parentcontrolname=" + strParentControlName;
    }
    if (name == null) { name = "divCodeHelpIFRAME"; }
    OpenPopupWindow(name, strTitle, strURL, "codehelp", winSize);
}
function OpenEditableField(content, tgt) {
    var posxx = 0;
    var posyy = 0;
    var delayedCode = '';

    posxx = xCurrent;
    posyy = yCurrent;

    CloseEditableField();
    OpenPopupWindow('divEditableField', null, SitePath + "/grid/edittemplate.aspx?tgt=" + tgt + "&content=" + content, "editablefield", "datepicker", content, posxx, posyy);

    delayedCode = "if (document.getElementById('cellResult') != null) { ";
    delayedCode += "document.getElementById('cellResult').focus();";
    delayedCode += "document.getElementById('cellResult').onkeydown = function(e) { ";
    delayedCode += "if (!e) var e = window.event; var pressedkey = (e.keyCode) ? e.keyCode : e.which; ";
    delayedCode += "if (pressedkey == 13) { document.getElementById('pickbutton').onclick(); } else { return true; } }";
    delayedCode += " } ";

    setTimeout(delayedCode, 150);
}

function CloseFilterWindow(name) {
    if (name == null) {
        if (GetBrowser() == 'IE6') {
            if (xFenster.instances.divFilterIFRAME) {
                xFenster.instances.divFilterIFRAME.hide();
                xFenster.instances.divFilterIFRAME.destroy();
            }
        }
        else {
            if (xFenster.instances.divFilter) {
                xFenster.instances.divFilter.hide();
                xFenster.instances.divFilter.destroy();
            }
        }
    }
    else {
        if (eval("xFenster.instances." + name)) {
            eval("xFenster.instances." + name + ".hide()");
            eval("xFenster.instances." + name + ".destroy()");
        }
    }
}
function CloseEditableField(name) {
    if (name == null) {
        if (GetBrowser() == 'IE6') {
            if (xFenster.instances.divEditableField) {
                xFenster.instances.divEditableField.hide();
            }
        }
        else {
            if (xFenster.instances.divEditableField) {
                xFenster.instances.divEditableField.hide();
            }
        }
    }
    else {
        if (eval("xFenster.instances." + name)) {
            eval("xFenster.instances." + name + ".hide()");
        }
    }
}
/*IF (TRUE) STATEMENTS IN THIS FUNCTION ENFORCES OPERATION OF CODEHELP TO BE IN IFRAME*/
function CloseCodeHelpWindow(name) {
   
    if (name == null) /*(GetBrowser() == 'IE6')*/
    {
        if (xFenster.instances.divCodeHelpIFRAME) {
            xFenster.instances.divCodeHelpIFRAME.hide();
            xFenster.instances.divCodeHelpIFRAME.destroy();
        }
    }
    else {
        if (eval("xFenster.instances." + name)) {
            eval("xFenster.instances." + name + ".hide()");
            eval("xFenster.instances." + name + ".destroy()");
        }
    }
}

function OpenDialog(ctrlName, strTitle, readOnly) {
    readOnly = (readOnly != null && readOnly == "true") ? 'true' : 'false';
    OpenPopupWindow('pop' + ctrlName, strTitle, SitePath + '/filter/codeHelpRemarks.aspx?targettextarea=' + ctrlName + '&readonly=' + readOnly, 'richtext');
}

function DoPopup() {
    if (window.parent != null && ((navigator.appVersion.indexOf("MSIE 6.0") >= 0 || navigator.appVersion.indexOf("MSIE 5.5") >= 0) && (navigator.appVersion.indexOf("MSIE 7.0") < 0 && navigator.appVersion.indexOf("MSIE 8.0") < 0))) {
        window.parent.name = 'sm7parentwindow';
        window.document.smfilterform.target = window.parent.name;
    }
    window.document.smfilterform.action = SitePath + '/filter/filtersubmit.aspx';
    window.document.smfilterform.submit();
}

function PopulateFilterCriteria(arrItemName, arrItemValue) {
    for (var i = 0; i < arrItemName.length; i++) {
        with (window.document.smfilterform) {
            elements[arrItemName[i]].value = arrItemValue[i];
        }
    }
}

function ResetPopupForm() {
    AjxGet(SitePath + "/webfunctions.aspx?" + encodeURI('method=clearfiltercriteria|params=none'), "divFilterClear", false);
    if (window.parent != null && (GetBrowser() == 'IE6')) {
        window.location.href = window.location.href;
    }
    else {
        OpenFilterWindow(strFilterWindowTitle, strFilterWindowURL, strFilterWindowSize);
    }
    return;
}

/********************************** POPUP ROUTINES END ******************************/

/********************************** XFENSTER START ******************************/

/*************** THE FOLLOWING CREDIT APPLIES FOR THE CODE USED IN THIS SECTION *********************/
/*																									*/
/*								Michael Foster (Cross-Browser.com)									*/
/*         X, a Cross-Browser Javascript Library. X is distributed under the terms of the GNU LGPL	*/
/*																									*/
/****************************************************************************************************/

/* Compiled from X 4.18 by XC 1.07 on 26Jul07 */
xLibrary = { version: '4.18', license: 'GNU LGPL', url: 'http://cross-browser.com/' }; function xCamelize(cssPropStr) { var i, c, a = cssPropStr.split('-'); var s = a[0]; for (i = 1; i < a.length; ++i) { c = a[i].charAt(0); s += a[i].replace(c, c.toUpperCase()); } return s; } function xClientHeight() { var v = 0, d = document, w = window; if ((!d.compatMode || d.compatMode == 'CSS1Compat') && !w.opera && d.documentElement && d.documentElement.clientHeight) { v = d.documentElement.clientHeight; } else if (d.body && d.body.clientHeight) { v = d.body.clientHeight; } else if (xDef(w.innerWidth, w.innerHeight, d.width)) { v = w.innerHeight; if (d.width > w.innerWidth) v -= 16; } return v; } function xClientWidth() { var v = 0, d = document, w = window; if ((!d.compatMode || d.compatMode == 'CSS1Compat') && !w.opera && d.documentElement && d.documentElement.clientWidth) { v = d.documentElement.clientWidth; } else if (d.body && d.body.clientWidth) { v = d.body.clientWidth; } else if (xDef(w.innerWidth, w.innerHeight, d.height)) { v = w.innerWidth; if (d.height > w.innerHeight) v -= 16; } return v; } function xDef() { for (var i = 0; i < arguments.length; ++i) { if (typeof (arguments[i]) == 'undefined') return false; } return true; } function xGetComputedStyle(e, p, i) { if (!(e = xGetElementById(e))) return null; var s, v = 'undefined', dv = document.defaultView; if (dv && dv.getComputedStyle) { s = dv.getComputedStyle(e, ''); if (s) v = s.getPropertyValue(p); } else if (e.currentStyle) { v = e.currentStyle[xCamelize(p)]; } else return null; return i ? (parseInt(v) || 0) : v; } function xGetElementById(e) { if (typeof (e) == 'string') { if (document.getElementById) e = document.getElementById(e); else if (document.all) e = document.all[e]; else e = null; } return e; } function xGetElementsByClassName(c, p, t, f) { var r = new Array(); var re = new RegExp("(^|\\s)" + c + "(\\s|$)"); var e = xGetElementsByTagName(t, p); for (var i = 0; i < e.length; ++i) { if (re.test(e[i].className)) { r[r.length] = e[i]; if (f) f(e[i]); } } return r; } function xGetElementsByTagName(t, p) { var list = null; t = t || '*'; p = p || document; if (typeof p.getElementsByTagName != 'undefined') { list = p.getElementsByTagName(t); if (t == '*' && (!list || !list.length)) list = p.all; } else { if (t == '*') list = p.all; else if (p.all && p.all.tags) list = p.all.tags(t); } return list || new Array(); } function xHasPoint(e, x, y, t, r, b, l) { if (!xNum(t)) { t = r = b = l = 0; } else if (!xNum(r)) { r = b = l = t; } else if (!xNum(b)) { l = r; b = t; } var eX = xPageX(e), eY = xPageY(e); return (x >= eX + l && x <= eX + xWidth(e) - r && y >= eY + t && y <= eY + xHeight(e) - b); } function xHeight(e, h) { if (!(e = xGetElementById(e))) return 0; if (xNum(h)) { if (h < 0) h = 0; else h = Math.round(h); } else h = -1; var css = xDef(e.style); if (e == document || e.tagName.toLowerCase() == 'html' || e.tagName.toLowerCase() == 'body') { h = xClientHeight(); } else if (css && xDef(e.offsetHeight) && xStr(e.style.height)) { if (h >= 0) { var pt = 0, pb = 0, bt = 0, bb = 0; if (document.compatMode == 'CSS1Compat') { var gcs = xGetComputedStyle; pt = gcs(e, 'padding-top', 1); if (pt !== null) { pb = gcs(e, 'padding-bottom', 1); bt = gcs(e, 'border-top-width', 1); bb = gcs(e, 'border-bottom-width', 1); } else if (xDef(e.offsetHeight, e.style.height)) { e.style.height = h + 'px'; pt = e.offsetHeight - h; } } h -= (pt + pb + bt + bb); if (isNaN(h) || h < 0) return; else e.style.height = h + 'px'; } h = e.offsetHeight; } else if (css && xDef(e.style.pixelHeight)) { if (h >= 0) e.style.pixelHeight = h; h = e.style.pixelHeight; } return h; } function xLeft(e, iX) { if (!(e = xGetElementById(e))) return 0; var css = xDef(e.style); if (css && xStr(e.style.left)) { if (xNum(iX)) e.style.left = iX + 'px'; else { iX = parseInt(e.style.left); if (isNaN(iX)) iX = xGetComputedStyle(e, 'left', 1); if (isNaN(iX)) iX = 0; } } else if (css && xDef(e.style.pixelLeft)) { if (xNum(iX)) e.style.pixelLeft = iX; else iX = e.style.pixelLeft; } return iX; } function xMoveTo(e, x, y) { xLeft(e, x); xTop(e, y); } function xNum() { for (var i = 0; i < arguments.length; ++i) { if (isNaN(arguments[i]) || typeof (arguments[i]) != 'number') return false; } return true; } function xOpacity(e, o) { var set = xDef(o); if (!(e = xGetElementById(e))) return 2; if (xStr(e.style.opacity)) { if (set) e.style.opacity = o + ''; else o = parseFloat(e.style.opacity); } else if (xStr(e.style.filter)) { if (set) e.style.filter = 'alpha(opacity=' + (100 * o) + ')'; else if (e.filters && e.filters.alpha) { o = e.filters.alpha.opacity / 100; } } else if (xStr(e.style.MozOpacity)) { if (set) e.style.MozOpacity = o + ''; else o = parseFloat(e.style.MozOpacity); } else if (xStr(e.style.KhtmlOpacity)) { if (set) e.style.KhtmlOpacity = o + ''; else o = parseFloat(e.style.KhtmlOpacity); } return isNaN(o) ? 1 : o; } function xPageX(e) { var x = 0; e = xGetElementById(e); while (e) { if (xDef(e.offsetLeft)) x += e.offsetLeft; e = xDef(e.offsetParent) ? e.offsetParent : null; } return x; } function xPageY(e) { var y = 0; e = xGetElementById(e); while (e) { if (xDef(e.offsetTop)) y += e.offsetTop; e = xDef(e.offsetParent) ? e.offsetParent : null; } return y; } function xResizeTo(e, w, h) { xWidth(e, w); xHeight(e, h); } function xScrollLeft(e, bWin) { var offset = 0; if (!xDef(e) || bWin || e == document || e.tagName.toLowerCase() == 'html' || e.tagName.toLowerCase() == 'body') { var w = window; if (bWin && e) w = e; if (w.document.documentElement && w.document.documentElement.scrollLeft) offset = w.document.documentElement.scrollLeft; else if (w.document.body && xDef(w.document.body.scrollLeft)) offset = w.document.body.scrollLeft; } else { e = xGetElementById(e); if (e && xNum(e.scrollLeft)) offset = e.scrollLeft; } return offset; } function xScrollTop(e, bWin) { var offset = 0; if (!xDef(e) || bWin || e == document || e.tagName.toLowerCase() == 'html' || e.tagName.toLowerCase() == 'body') { var w = window; if (bWin && e) w = e; if (w.document.documentElement && w.document.documentElement.scrollTop) offset = w.document.documentElement.scrollTop; else if (w.document.body && xDef(w.document.body.scrollTop)) offset = w.document.body.scrollTop; } else { e = xGetElementById(e); if (e && xNum(e.scrollTop)) offset = e.scrollTop; } return offset; } function xStr(s) { for (var i = 0; i < arguments.length; ++i) { if (typeof (arguments[i]) != 'string') return false; } return true; } function xStyle(sProp, sVal) { var i, e; for (i = 2; i < arguments.length; ++i) { e = xGetElementById(arguments[i]); if (e.style) { try { e.style[sProp] = sVal; } catch (err) { e.style[sProp] = ''; } } } } function xTop(e, iY) { if (!(e = xGetElementById(e))) return 0; var css = xDef(e.style); if (css && xStr(e.style.top)) { if (xNum(iY)) e.style.top = iY + 'px'; else { iY = parseInt(e.style.top); if (isNaN(iY)) iY = xGetComputedStyle(e, 'top', 1); if (isNaN(iY)) iY = 0; } } else if (css && xDef(e.style.pixelTop)) { if (xNum(iY)) e.style.pixelTop = iY; else iY = e.style.pixelTop; } return iY; } function xWidth(e, w) { if (!(e = xGetElementById(e))) return 0; if (xNum(w)) { if (w < 0) w = 0; else w = Math.round(w); } else w = -1; var css = xDef(e.style); if (e == document || e.tagName.toLowerCase() == 'html' || e.tagName.toLowerCase() == 'body') { w = xClientWidth(); } else if (css && xDef(e.offsetWidth) && xStr(e.style.width)) { if (w >= 0) { var pl = 0, pr = 0, bl = 0, br = 0; if (document.compatMode == 'CSS1Compat') { var gcs = xGetComputedStyle; pl = gcs(e, 'padding-left', 1); if (pl !== null) { pr = gcs(e, 'padding-right', 1); bl = gcs(e, 'border-left-width', 1); br = gcs(e, 'border-right-width', 1); } else if (xDef(e.offsetWidth, e.style.width)) { e.style.width = w + 'px'; pl = e.offsetWidth - w; } } w -= (pl + pr + bl + br); if (isNaN(w) || w < 0) return; else e.style.width = w + 'px'; } w = e.offsetWidth; } else if (css && xDef(e.style.pixelWidth)) { if (w >= 0) e.style.pixelWidth = w; w = e.style.pixelWidth; } return w; }

/* Compiled from X 4.18 by XC 1.07 on 26Jul07 */
function xEvent(evt) { var e = evt || window.event; if (!e) return; this.type = e.type; this.target = e.target || e.srcElement; this.relatedTarget = e.relatedTarget; /*@cc_onif (e.type == 'mouseover') this.relatedTarget = e.fromElement; else if (e.type == 'mouseout') this.relatedTarget = e.toElement; @*/if (xDef(e.pageX)) { this.pageX = e.pageX; this.pageY = e.pageY; } else if (xDef(e.clientX)) { this.pageX = e.clientX + xScrollLeft(); this.pageY = e.clientY + xScrollTop(); } if (xDef(e.offsetX)) { this.offsetX = e.offsetX; this.offsetY = e.offsetY; } else if (xDef(e.layerX)) { this.offsetX = e.layerX; this.offsetY = e.layerY; } else { this.offsetX = this.pageX - xPageX(this.target); this.offsetY = this.pageY - xPageY(this.target); } this.keyCode = e.keyCode || e.which || 0; this.shiftKey = e.shiftKey; this.ctrlKey = e.ctrlKey; this.altKey = e.altKey; if (typeof e.type == 'string') { if (e.type.indexOf('click') != -1) { this.button = 0; } else if (e.type.indexOf('mouse') != -1) { this.button = e.button; /*@cc_onif (e.button & 1) this.button = 0; else if (e.button & 4) this.button = 1; else if (e.button & 2) this.button = 2; @*/ } } } xLibrary = { version: '4.18', license: 'GNU LGPL', url: 'http://cross-browser.com/' }; function xAddEventListener(e, eT, eL, cap) { if (!(e = xGetElementById(e))) return; eT = eT.toLowerCase(); if (e.addEventListener) e.addEventListener(eT, eL, cap || false); else if (e.attachEvent) e.attachEvent('on' + eT, eL); else { var o = e['on' + eT]; e['on' + eT] = typeof o == 'function' ? function (v) { o(v); eL(v); } : eL; } } function xPreventDefault(e) { if (e && e.preventDefault) e.preventDefault(); else if (window.event) window.event.returnValue = false; } function xRemoveEventListener(e, eT, eL, cap) { if (!(e = xGetElementById(e))) return; eT = eT.toLowerCase(); if (e.removeEventListener) e.removeEventListener(eT, eL, cap || false); else if (e.detachEvent) e.detachEvent('on' + eT, eL); else e['on' + eT] = null; } function xStopPropagation(evt) { if (evt && evt.stopPropagation) evt.stopPropagation(); else if (window.event) window.event.cancelBubble = true; }

/* xEnableDrag r8, Copyright 2002-2007 Michael Foster (Cross-Browser.com)*/
/* Part of X, a Cross-Browser Javascript Library, Distributed under the terms of the GNU LGPL*/

function xEnableDrag(id, fS, fD, fE) {
    var mx = 0, my = 0, el = xGetElementById(id);
    if (el) {
        el.xDragEnabled = true;
        xAddEventListener(el, 'mousedown', dragStart, false);
    }
    /* Private Functions*/
    function dragStart(e) {
        if (el.xDragEnabled) {
            var ev = new xEvent(e);
            xPreventDefault(e);
            mx = ev.pageX;
            my = ev.pageY;
            xAddEventListener(document, 'mousemove', drag, false);
            xAddEventListener(document, 'mouseup', dragEnd, false);
            if (fS) {
                fS(el, ev.pageX, ev.pageY, ev);
            }
        }
    }

    function drag(e) {
        var ev, dx, dy;
        xPreventDefault(e);
        ev = new xEvent(e);
        dx = ev.pageX - mx;
        dy = ev.pageY - my;
        mx = ev.pageX;
        my = ev.pageY;
        if (fD) {
            fD(el, dx, dy, ev);
        }
        else {
            xMoveTo(el, xLeft(el) + dx, xTop(el) + dy);
        }
    }

    function dragEnd(e) {
        var ev = new xEvent(e);
        xPreventDefault(e);
        xRemoveEventListener(document, 'mouseup', dragEnd, false);
        xRemoveEventListener(document, 'mousemove', drag, false);
        if (fE) {
            fE(el, ev.pageX, ev.pageY, ev);
        }
        if (xEnableDrag.drop) {
            xEnableDrag.drop(el, ev);
        }
    }
}

xEnableDrag.drops = []; /* static property */

/* xFenster r17, Copyright 2004-2007 Michael Foster (Cross-Browser.com)*/
/* Part of X, a Cross-Browser Javascript Library, Distributed under the terms of the GNU LGPL*/

function xFenster(clientId, iniTitle, iniUrl, iniX, iniY, iniW, iniH,
                  miniW, fenceId, conPad, conBor, cliBor,
                  enMove, enResize, enMinimize, enMaximize, enClose, enStatus, enFixed,
                  fnMove, fnResize, fnMinimize, fnMaximize, fnRestore, fnClose, fnFocus, fnLoad,
                  clsCon, clsCli, clsTB, clsTBF, clsSB, clsSBF,
                  clsRI, clsNI, clsMI, clsOI, clsCI,
                  txtResize, txtMin, txtMax, txtRestore, txtClose) {
    var me = this;

    /* Public Methods*/

    me.paint = function (dw, dh) {
        me.conW += dw;
        me.conH += dh;
        me.con.style.width = (me.conW - B2) + 'px';
        me.con.style.height = (me.conH - B2) + 'px';
        /*@cc_on
        @if (@_jscript)
        xWidth(me.tbar, me.conW - M2 - B2);
        if (!me.minimized && me.sbar) {
            xWidth(me.sbar, me.conW - M2 - B2);
            me.sbar.style.top = me.conH - me.sbar.offsetHeight - M - B2;
        }
        @end@*/
        if (!me.minimized) {
            me.client.style.top = (M + me.tbar.offsetHeight) + 'px';
            me.client.style.width = (me.conW - M2 - B2 - cB2) + 'px';
            me.client.style.height = (me.conH - me.tbar.offsetHeight - (me.sbar ? me.sbar.offsetHeight : 0) - M2 - B2 - cB2) + 'px';
        }
    };

    me.focus = function () {
        if (xFenster.focused != me && (!fnFocus || fnFocus(me))) {
            me.con.style.zIndex = xFenster.nextZ++;
            if (xFenster.focused) {
                xFenster.focused.tbar.className = xFenster.focused.clsTB;
                if (xFenster.focused.sbar) { xFenster.focused.sbar.className = xFenster.focused.clsSB; }
            }
            me.tbar.className = clsTBF;
            if (me.sbar) { me.sbar.className = clsSBF; }
            xFenster.focused = me;
        }
    };

    me.href = function (s) {
        var h = s;
        if (me.isIFrame) {
            if (me.client.contentWindow) {
                if (s) { me.client.contentWindow.location = s; }
                else { h = me.client.contentWindow.location.href; }
            }
            else if (typeof me.client.src == 'string') { /* for Safari/Apollo/WebKit on Windows*/
                if (s) { me.client.src = s; }
                else { h = me.client.src; }
            }
        }
        return h;
    };

    me.hide = function (e) {
      
        var dvOverlay = document.getElementById('overlay');
        if (dvOverlay != null)
            dvOverlay.style.display = 'none';

        var i, o = xFenster.instances, z = 0, hz = 0, f = null;
        if (!fnClose || fnClose(me)) {
            me.con.style.display = 'none';
            me.hidden = true;
            xStopPropagation(e);
            if (me == xFenster.focused) {
                for (i in o) { /* find the next appropriate fenster to focus*/
                    if (o.hasOwnProperty(i) && o[i] && !o[i].hidden && o[i] != me) {
                        z = parseInt(o[i].con.style.zIndex);
                        if (z > hz) {
                            hz = z;
                            f = o[i];
                        }
                    }
                }
                if (f) { f.focus(); }
            }
        }
    };

    me.show = function () {
        me.con.style.display = 'block';
        me.hidden = false;
        me.focus();
    };

    me.status = function (s) {
        if (me.sbar) {
            if (s) { me.sbar.innerHTML = s; }
            else { return me.sbar.innerHTML; }
        }
    };

    me.title = function (s) {
        if (s) { me.tbar.innerHTML = s; }
        else { return me.tbar.innerHTML; }
    };

    me.destroy = function () {

        try {
            me.hide();
            xRemoveEventListener(window, 'unload', winUnload, false);
            xRemoveEventListener(window, 'resize', winResize, false);
            me.con.parentNode.removeChild(me.con);
            winUnload();
        }
        catch (e) {
            alert('Error in destroy:\n\n' + e); /* dbg */
        }
    };

    me.minimize = function () {
        if (enMinimize && !me.minimized && !me.hidden) {
            minClick();
            return true;
        }
        return false;
    };

    me.maximize = function () {
        if (enMaximize && !me.maximized && !me.hidden) {
            maxClick();
            return true;
        }
        return false;
    };

    me.restore = function () {
        var b, i, t;
        if (me.maximized) {
            b = me.mbtn;
            b.onclick = maxClick;
            i = clsMI;
            t = txtMax || '';
            me.maximized = !me.maximized;
        }
        else if (me.minimized) {
            b = me.nbtn;
            b.onclick = minClick;
            i = clsNI;
            t = txtMin || '';
            me.minimized = !me.minimized;
            me.client.style.display = 'block';
            if (me.sbar) { me.sbar.style.display = 'block'; }
        }
        else {
            return false;
        }
        xMoveTo(me.con, rX, rY);
        b.className = i;
        b.title = t;
        if (me.rbtn) { me.rbtn.style.display = 'block'; }
        me.conW = rW;
        me.conH = rH;
        me.paint(0, 0);
        if (fnRestore) { fnRestore(me); }
        return true;
    };

    /* Private Event Listeners*/

    function dragStart() {
        var i, o = xFenster.instances;
        for (i in o) {
            if (o.hasOwnProperty(i) && o[i] && !o[i].minimized && o[i].isIFrame) {
                o[i].client.style.display = 'none';
            }
        }
    }

    function dragEnd() {
        var i, o = xFenster.instances;
        for (i in o) {
            if (o.hasOwnProperty(i) && o[i] && !o[i].minimized && o[i].isIFrame) {
                o[i].client.style.display = 'block';
            }
        }

        /*<BEGIN CODE ADDED ON SEP 30 2008 TO PREVENT DISAPPEARING OF FENSTERS>*/

        if (Number(me.con.style.top.replace('px', '')) < 0) {
            me.con.style.top = '5px';
        }

        if (Number(me.con.style.left.replace('px', '')) < 0) {
            me.con.style.left = '5px';
        }

        /*</CODE ADDED ON SEP 30 2008 TO PREVENT DISAPPEARING OF FENSTERS>*/
    }

    function barDrag(e, mdx, mdy) {
        var x = me.con.offsetLeft + mdx;
        var y = me.con.offsetTop + mdy;
        if (!fnMove || fnMove(me, x, y)) {
            if (!fenceId || inFence(x, y, me.con.offsetWidth, me.con.offsetHeight)) {
                me.con.style.left = x + 'px';
                me.con.style.top = y + 'px';
            }
        }
    }
    function resDrag(e, mdx, mdy) {
        var w = me.con.offsetWidth + mdx;
        var h = me.con.offsetHeight + mdy;
        if (w >= 100 && h >= 50) {
            if (!fnResize || fnResize(me, w, h)) {
                if (!fenceId || inFence(me.con.offsetLeft, me.con.offsetTop, w, h)) {
                    me.paint(mdx, mdy);
                }
            }
        }
    }

    function maxClick() {
        var f, fx, fy, x = 0, y = 0, w, h, fx2, fy2;
        var sl = xScrollLeft(), st = xScrollTop();
        var cw = xClientWidth(), ch = xClientHeight();
        if (fenceId) {
            f = xGetElementById(fenceId);
            x = fx = xPageX(f);
            y = fy = xPageY(f);
            w = f.offsetWidth;
            h = f.offsetHeight;
            if (enFixed) {
                x = fx - sl;
                if (x < 0) { x = 0; }
                y = fy - st;
                if (y < 0) { y = 0; }

                fx2 = fx + w;
                if (fx2 >= sl + cw) { fx2 = cw; }
                fy2 = fy + h;
                if (fy2 >= st + ch) { fy2 = ch; }

                w = fx2 - x;
                h = fy2 - y;
            }
        }
        else {
            if (!enFixed) {
                x = sl;
                y = st;
            }
            w = cw;
            h = ch;
        }
        if (!fnMaximize || fnMaximize(me, w - M2 - B2, h - me.tbar.offsetHeight - (me.sbar ? me.sbar.offsetHeight : 0) - M2 - B2)) {
            me.restore();
            me.maximized = !me.maximized;
            minmax(me.mbtn, w, h, x, y);
        }
    }

    function minClick() {
        var r = 1, x = 2, y, h, i, o = xFenster.instances;
        if (!fnMinimize || fnMinimize(me)) {
            for (i in o) {
                if (o.hasOwnProperty(i) && o[i] && o[i].minimized) {
                    x += o[i].con.offsetWidth + 2;
                    if (x + miniW > xClientWidth()) {
                        x = 2;
                        ++r;
                    }
                }
            }
            h = me.tbar.offsetHeight + M2 + B2;
            y = xClientHeight() - (r * (h + 2));
            if (!enFixed) { y += xScrollTop(); }
            me.restore();
            me.client.style.display = 'none';
            if (me.sbar) { me.sbar.style.display = 'none'; }
            me.minimized = !me.minimized;
            minmax(me.nbtn, miniW, h, x, y);
        }
    }

    function winResize() {
        if (me.maximized) {
            xResizeTo(me.con, 100, 100); /* ensure fenster isn't causing scrollbars*/
            if (!enFixed) { xMoveTo(me.con, xScrollLeft(), xScrollTop()); }
            me.conW = xClientWidth();
            me.conH = xClientHeight();
            me.paint(0, 0);
        }
    }

    function winUnload() {

        me.con.onmousedown = me.con.onclick = null;
        if (me.nbtn) { me.nbtn.onclick = null; }
        if (me.mbtn) { me.mbtn.onclick = me.tbar.ondblclick = null; }
        if (me.cbtn) { me.cbtn.onclick = me.cbtn.onmousedown = null; }
        xFenster.instances[clientId] = null;
        me = null;
    }

    /* Private Functions*/

    function minmax(b, w, h, x, y) {
        rW = me.con.offsetWidth;
        rH = me.con.offsetHeight;
        rX = me.con.offsetLeft;
        rY = me.con.offsetTop;
        if (x != -1) { xMoveTo(me.con, x, y); }
        b.className = clsOI;
        b.title = txtRestore || '';
        b.onclick = me.restore;
        if (me.rbtn) { me.rbtn.style.display = 'none'; }
        me.conW = w;
        me.conH = h;
        me.paint(0, 0);
    }

    function inFence(x, y, w, h) {
        var f = xGetElementById(fenceId);
        if (enFixed) {
            x += xScrollLeft();
            y += xScrollTop();
        }
        return (!(x < f.offsetLeft || x + w > f.offsetLeft + f.offsetWidth || y < f.offsetTop || y + h > f.offsetTop + f.offsetHeight));
    }

    /* Constructor Code*/

    xFenster.instances[clientId] = this;

    /* public properties*/
    me.con = null;  /* outermost container */
    me.tbar = null; /* title bar */
    me.sbar = null; /* status bar */
    me.rbtn = null; /* resize icon */
    me.nbtn = null; /* minimize icon */
    me.mbtn = null; /* maximize icon */
    me.cbtn = null; /* close icon */

    me.minimized = false;
    me.maximized = false;
    me.hidden = false;

    me.isIFrame = (typeof iniUrl == 'string');
    me.client = xGetElementById(clientId);
    me.clsSB = clsSB;
    me.clsTB = clsTB;
    me.conW = iniW;
    me.conH = iniH;

    /* private properties*/
    var rX, rY, rW, rH; /* "restore" values*/
    var M = conPad, B = conBor, cB = cliBor;
    var M2 = 2 * M, B2 = 2 * B, cB2 = 2 * cB;

    /* create elements*/
    if (!me.client) {
        me.client = document.createElement(me.isIFrame ? 'iframe' : 'div');
        me.client.id = clientId;
    }
    me.client.className += ' ' + clsCli;
    me.con = document.createElement('div');

    me.con.className = clsCon;
    if (enResize) {
        me.rbtn = document.createElement('div');
        me.rbtn.className = clsRI;
        me.rbtn.title = txtResize || '';
    }
    if (enMinimize) {
        me.nbtn = document.createElement('div');
        me.nbtn.className = clsNI;
        me.nbtn.title = txtMin || '';
    }
    if (enMaximize) {
        me.mbtn = document.createElement('div');
        me.mbtn.className = clsMI;
        me.mbtn.title = txtMax || '';
    }

    if (enClose) {
        me.cbtn = document.createElement('div');
        me.cbtn.className = clsCI;
        me.cbtn.title = txtClose || '';
    }

    me.tbar = document.createElement('div');
    me.tbar.className = clsTB;
    me.title(iniTitle);
    if (enStatus) {
        me.sbar = document.createElement('div');
        me.sbar.className = clsSB;
        me.status('&nbsp;');
    }

    /* append elements*/
    me.con.appendChild(me.tbar);
    if (me.nbtn) { me.con.appendChild(me.nbtn); }
    if (me.mbtn) { me.con.appendChild(me.mbtn); }
    if (me.cbtn) { me.con.appendChild(me.cbtn); }
    me.con.appendChild(me.client);
    if (me.sbar) { me.con.appendChild(me.sbar); }
    if (me.rbtn) { me.con.appendChild(me.rbtn); }
    document.body.appendChild(me.con);
    /* position and paint the fenster IE6 or down*/
    /*@cc_on
    @if (@_jscript_version <= 5.6)
    enFixed = false;
    @else @*/
      if (enFixed) me.con.style.position = 'fixed';
  /*@end
    @*/
    me.con.style.borderWidth = conBor || 0;
    me.client.style.borderWidth = cliBor || 0;
    me.client.style.display = 'block'; /* do this before paint*/
    me.client.style.visibility = 'visible';
    me.tbar.style.left = me.tbar.style.right = me.tbar.style.top = M + 'px';
    if (me.sbar) { me.sbar.style.left = me.sbar.style.right = me.sbar.style.bottom = M + 'px'; }
    me.client.style.left = M + 'px';
    xMoveTo(me.con, iniX, iniY);
    me.paint(0, 0);
    /* position icons*/
    var t = M + B, r = t;
    if (me.cbtn) {
        me.cbtn.style.top = t + 'px';
        me.cbtn.style.right = r + 'px';
        r += me.cbtn.offsetWidth + 2;
    }
    if (me.mbtn) {
        me.mbtn.style.top = t + 'px';
        me.mbtn.style.right = r + 'px';
        r += me.mbtn.offsetWidth + 2;
    }
    if (me.nbtn) {
        me.nbtn.style.top = t + 'px';
        me.nbtn.style.right = r + 'px';
    }
    if (me.rbtn) {
        me.rbtn.style.right = me.rbtn.style.bottom = t + 'px';
    }
    /* register event listeners*/
    if (me.isIFrame) {
        me.href(iniUrl);
        if (fnLoad) { xAddEventListener(me.client, 'load', function () { fnLoad(me); }, false); }
        me.client.name = clientId;
    }
    if (enMove) { xEnableDrag(me.tbar, dragStart, barDrag, dragEnd); }
    if (enResize) { xEnableDrag(me.rbtn, dragStart, resDrag, dragEnd); }
    me.con.onmousedown = me.focus;
    if (enMinimize) { me.nbtn.onclick = minClick; }
    if (enMaximize) { me.mbtn.onclick = me.tbar.ondblclick = maxClick; }
    if (enClose) {
        me.cbtn.onclick = me.hide;
        me.cbtn.onmousedown = xStopPropagation;
    }
    xAddEventListener(window, 'unload', winUnload, false);
    xAddEventListener(window, 'resize', winResize, false);
    /* show the fenster*/
    me.con.style.visibility = 'visible';
    me.focus();

    /*<BEGIN CODE ADDED ON SEP 30 2008 TO PREVENT DISAPPEARING OF FENSTERS>*/
    dragEnd();
    /*</END CODE ADDED ON SEP 30 2008 TO PREVENT DISAPPEARING OF FENSTERS>*/


} /* end xFenster object prototype*/

/* xFenster static properties*/
xFenster.nextZ = 1000;
xFenster.focused = null;
xFenster.instances = {};

function getCenterCor(w, h) {
    var X = 0;
    var Y = 0;
    var x1 = 0;
    var y1 = 0;
    if (self.innerWidth) {
        X = self.innerWidth; x1 = window.pageXOffset;
        Y = self.innerHeight; y1 = window.pageYOffset;
    }
    else if (document.documentElement && document.documentElement.clientWidth) {
        X = document.documentElement.clientWidth; x1 = document.documentElement.scrollLeft;
        Y = document.documentElement.clientHeight; y1 = document.documentElement.scrollTop;
    }
    else if (document.body) {
        X = document.body.clientWidth; x1 = document.documentElement.scrollLeft;
        Y = document.body.clientHeight; y1 = document.documentElement.scrollTop;
    }
    /*calculate the center position */
    var cor = new Object();
    cor.x = ((X / 2) - (w / 2)) + x1;
    cor.y = ((Y / 2) - (h / 2)) + y1;
    return cor;
}
/********************************** XFENSTER END ******************************/

/********************************** DEPENDENCIES AND DEBUGGING START ******************************/
//CheckDependencies();
function CheckDependencies() {
    try {
        GetBrowser();
    }
    catch (e) {
        ServerTrappableJSError('Dependencies not loaded for: ' + window.location.href);
    }
}

function ServerTrappableJSError(message) {
    try {
        window.location.href = SitePath + "/jserror.aspx?from=" + encodeURI(window.location.href) + "&message=" + encodeURI(message);
    }
    catch (e) {
        alert("Error from URL: " + window.location.href + "\n\n***********************\n\n" + message);
    }

    return;
}
/********************************** DEPENDENCIES AND DEBUGGING END ******************************/


var xCurrent, yCurrent;
var ieMouseMove = (parseFloat(navigator.appVersion) >= 4 && navigator.appName == "Microsoft Internet Explorer") ? true : false;

document.onmousemove = DoMouseMove;

function GetMouseCoordinates(event) {
    var coord;
    ev = event || window.event;
    if (ieMouseMove) {
        coord = ev.x + ',' + ev.y;
    }
    else {
        coord = ev.pageX + ',' + ev.pageY;
    }
    return coord.split(',')[0] + ',' + coord.split(',')[1];
}

function DoMouseMove(event) {
    var coord = '';
    coord = GetMouseCoordinates(event);
    xCurrent = coord.split(',')[0];
    yCurrent = coord.split(',')[1];
}

function fensterresize(me) {
    try {
        if (loadErrMsg != null) {
            loadErrMsg(me);
        }
    } catch (e) { }
    if (me.isIFrame) {
        var ht = me.client.contentWindow.document.body.scrollHeight + 20;
        var wd = me.client.contentWindow.document.body.scrollWidth + 20;
        if (ht < 1000 && wd < 800) {
            me.conW = me.client.contentWindow.document.body.scrollWidth + 20;
            me.conH = me.client.contentWindow.document.body.scrollHeight + 20;
            me.paint(35, 35);
        }
    }
    else {
        var ht = me.con.scrollWidth + 20;
        var wd = me.con.scrollHeight + 20;
        if (ht < 1000 && wd < 800) {
            me.conW = me.con.scrollWidth + 20;
            me.conH = me.con.scrollHeight + 20;
            me.paint(35, 35);
        }
    }
    var rtc = getCenterCor(me.con.style.width.replace("px", ""), me.con.style.height.replace("px", ""));
    me.con.style.top = rtc.y + "px";
    me.con.style.left = rtc.x + "px";
}

/* This method is used in fuel/oil consumption */
function SetValue(name) {
    var cmb = document.getElementById("cmb" + name);
    document.getElementById("hid" + name).value = cmb.options[cmb.selectedIndex].text;
}

function TreeDiv(divName, txtObj) {
    var div = document.getElementById(divName);
    if (div != null) {
        div.style.height = "150px";
        div.style.display = (div.style.display == "none") ? "inline" : "none";
        if (div.innerHTML == "")
            AjxGet(SitePath + '/codehelpcomponenttree.aspx', 'divTree', true);
    }
}

function hddivTree(divName) {
    var div = document.getElementById(divName);
    if (div != null) {
        div.style.display = "none";
    }
}

function sv(name, val) {
    document.getElementById("txtComponentName").value = name;
    document.getElementById("hidComponentCode").value = val;
    hddivTree('divTree');
}

function OpenCaption(obj, code) {
    obj.setAttribute("c", code);
    obj.onmouseup = function (e) {
        if (!e) var e = (window.event) ? window.event : e;
        CapUpdate(this, e.altKey);
    }
}

function CapUpdate(obj, k) {
    if (k)
        OpenGeneralWindow('Caption', SitePath + '/administration/updatecaption.aspx?c=' + obj.getAttribute("c"), null, 'caption', false, null, null, 500, 200);
}