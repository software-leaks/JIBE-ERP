function showReportissueModal(evt, TargetctlID) {
    document.getElementById('popupContact').style.display = "block";
    //document.getElementById(TargetctlID).style.display = "block";
    SetPosition_ReportIssueModal(evt, TargetctlID);

}


function SetPosition_ReportIssueModal(evt, TargetctlID) {
   
    var targObj = document.getElementById(TargetctlID);
    var pageX = 0;
    var pageY = 0;

    if ('pageX' in evt) { // all browsers except IE before version 9
        pageX = evt.pageX;
        pageY = evt.pageY;
    }
    else {  // IE before version 9
        pageX = evt.clientX + document.documentElement.scrollLeft;
        pageY = evt.clientY + document.documentElement.scrollTop;
    }

    var pageHeight = document.body.scrollHeight;
    var height = targObj.clientHeight;

    var pageWidth = document.body.scrollWidth;
    var width = targObj.clientWidth;

    if (pageHeight < (height + pageY)) {

        if (pageY - height < 0)
            targObj.style.top = "5px";
        else
            targObj.style.top = (pageY - height) + "px";
    }
    else {
        targObj.style.top = (pageY + 5) + "px";
    }

    if (pageWidth < (width + pageX)) {

        targObj.style.left = (pageX - (width + 10)) + "px";
    }
    else {
        targObj.style.left = (pageX + 10) + "px";
    }
}

function hideReportIssueModal() {
    document.getElementById('popupContact').style.display = "none";
    //document.getElementById('dvIframeReportIssue').style.display = "none";
}