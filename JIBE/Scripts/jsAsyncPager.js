
function Asyncpager_CreatePages(lblPaging, lblTotalPages, CurrentPageIndex, PageSize, CountTotalRec, CurrentPageSection, BindDataItem) {

    var obj_lblPaging = document.getElementById(lblPaging);
    var obj_lblTotalPages = document.getElementById(lblTotalPages);
    var obj_CurrentPageIndex = document.getElementById(CurrentPageIndex);
    var obj_PageSize = document.getElementById(PageSize);
    var obj_CountTotalRec = document.getElementById(CountTotalRec);
    var obj_CurrentPageSection = document.getElementById(CurrentPageSection);

    var val_CurrentPageIndex = parseInt(document.getElementById(CurrentPageIndex).value);
    var val_PageSize = parseInt(document.getElementById(PageSize).value);
    var val_CountTotalRec = parseInt(document.getElementById(CountTotalRec).value);
    var val_CurrentPageSection = parseInt(document.getElementById(CurrentPageSection).value);

    if (((val_CurrentPageIndex * val_PageSize) - (val_PageSize - 1)) > val_CountTotalRec && val_CurrentPageIndex != 1) {
        obj_CurrentPageIndex.value = 1;
        obj_CurrentPageSection.value = 1;
        BindDataItem();
    }


    obj_lblPaging.innerHTML = "";
    var ipg = parseInt(val_CurrentPageSection);
    obj_lblTotalPages.innerHTML = "";

    if (val_CountTotalRec != 0) {

        var TotalPages = parseFloat((parseFloat(parseFloat(val_CountTotalRec) / parseFloat(val_PageSize)).toString().replace(/^[^\.]+/, '0'))) > 0 ? parseInt(((val_CountTotalRec / val_PageSize) + 1)) : parseInt(val_CountTotalRec / val_PageSize);
        var tillLoop = val_CurrentPageSection + 10 > TotalPages ? TotalPages : val_CurrentPageSection + 10;

        obj_lblTotalPages.innerHTML = " [ Total Pages : " + TotalPages.toString() + "&nbsp;&nbsp; Records found :" + val_CountTotalRec.toString() + " ] ";
       
        for (var k = 0; k < 10; k++) {
            if (ipg <= tillLoop) {
                var cpg = ipg.toString(); ;
                var lnk = document.createElement("a");
                lnk.name = "lnk" + cpg;
                lnk.cpgnumber = cpg;

                lnk.className = "Paging-Custom";
                var tpg = document.createTextNode(cpg);
                lnk.appendChild(tpg);

                lnk.onclick = function () { Asyncpager_Page_List(CurrentPageIndex, PageSize, CountTotalRec, CurrentPageSection, this.cpgnumber, BindDataItem); return false; };

                if (ipg == val_CurrentPageIndex) {
                    lnk.className = "Paging-Custom Paging-Selected";


                }
                obj_lblPaging.appendChild(lnk);
                ipg++;
            }
        }

    }

}



function Asyncpager_Page_List(CurrentPageIndex, PageSize, CountTotalRec, CurrentPageSection, command, BindDataItem) {


    var obj_CurrentPageIndex = document.getElementById(CurrentPageIndex);

    var val_CurrentPageIndex = parseInt(document.getElementById(CurrentPageIndex).value);
    var val_PageSize = parseInt(document.getElementById(PageSize).value);
    var val_CountTotalRec = parseInt(document.getElementById(CountTotalRec).value);
    var val_CurrentPageSection = parseInt(document.getElementById(CurrentPageSection).value);

    if (command == "prev") {

        if ((val_CurrentPageIndex - 1) > 0) {
            obj_CurrentPageIndex.value = val_CurrentPageSection;
        }

    }

    else if (command == "next") {

        if (val_CurrentPageIndex * val_PageSize < val_CountTotalRec) {
            obj_CurrentPageIndex.value = val_CurrentPageSection;
        }

    }

    else if (command == "last") {

        if (val_CurrentPageIndex * val_PageSize < val_CountTotalRec) {
            obj_CurrentPageIndex.value = parseInt(val_CountTotalRec / val_PageSize);
        }

    }

    else if (command == "first") {

        obj_CurrentPageIndex.value = 1;

    }

    if (!isNaN(command)) {

        obj_CurrentPageIndex.value = command;
    }

    BindDataItem();

}


function Asyncpager_BuildPager(BindDataItem, pagerControlID) {

    var lblPaging = pagerControlID + "_lblPaging";
    var lblTotalPages = pagerControlID + "_lblTotalPages";
    var CurrentPageIndex = pagerControlID + "_hdfPageIndex";
    var PageSize = pagerControlID + "_hdfPageSize";
    var CountTotalRec = pagerControlID + "_hdfcountTotalRec";
    var CurrentPageSection = pagerControlID + "_hdfCurrentPageSection";

    var obj_first = document.getElementById(pagerControlID + "_first");
    var obj_prev = document.getElementById(pagerControlID + "_prev");
    var obj_next = document.getElementById(pagerControlID + "_next");
    var obj_last = document.getElementById(pagerControlID + "_last");

    var val_CurrentPageIndex = parseInt(document.getElementById(CurrentPageIndex).value);
    var val_PageSize = parseInt(document.getElementById(PageSize).value);
    var val_CountTotalRec = parseInt(document.getElementById(CountTotalRec).value);
    var val_CurrentPageSection = parseInt(document.getElementById(CurrentPageSection).value);


    if (((val_CurrentPageIndex) - 1) > 0 && val_CurrentPageSection > 1) {
        obj_prev.style.display = "inline";
        obj_first.style.display = "inline";
    }
    else {
        obj_prev.style.display = "none";
        obj_first.style.display = "none";
    }

    if (val_CurrentPageIndex * val_PageSize > val_CountTotalRec || (val_CurrentPageSection + 9) * val_PageSize > val_CountTotalRec) {
        obj_next.style.display = "none";
        obj_last.style.display = "none";
    }
    else {
        obj_next.style.display = "inline";
        obj_last.style.display = "inline";
    }

    Asyncpager_CreatePages(lblPaging, lblTotalPages, CurrentPageIndex, PageSize, CountTotalRec, CurrentPageSection, BindDataItem);

}


function Asyncpager_Navigate_Sections(CurrentPageIndex, PageSize, CountTotalRec, CurrentPageSection, command, BindDataItem) {

    var obj_CurrentPageSection = document.getElementById(CurrentPageSection);
    var obj_CurrentPageIndex = document.getElementById(CurrentPageIndex);

    var val_CurrentPageIndex = parseInt(document.getElementById(CurrentPageIndex).value);
    var val_PageSize = parseInt(document.getElementById(PageSize).value);
    var val_CountTotalRec = parseInt(document.getElementById(CountTotalRec).value);
    var val_CurrentPageSection = parseInt(document.getElementById(CurrentPageSection).value);

    if (command == "prev") {

        obj_CurrentPageSection.value = val_CurrentPageSection - 10;

    }

    else if (command == "next") {

        obj_CurrentPageSection.value = val_CurrentPageSection + 10;
    }

    else if (command == "last") {

        var TotalPages = parseFloat((parseFloat(parseFloat(val_CountTotalRec) / parseFloat(val_PageSize)).toString().replace(/^[^\.]+/, '0'))) > 0 ? parseInt(((val_CountTotalRec / val_PageSize) + 1)) : parseInt(val_CountTotalRec / val_PageSize);
        obj_CurrentPageSection.value = (parseFloat((parseFloat(TotalPages) / 10).toString().replace(/^[^\.]+/, '0')) > 0 ? parseInt(((parseInt(TotalPages / 10) + 1) * 10) - 9) : parseInt(TotalPages - 9));
        obj_CurrentPageIndex.value = TotalPages;


    }

    else if (command == "first") {

        obj_CurrentPageSection.value = 1;
        obj_CurrentPageIndex.value = 1;

    }

    Asyncpager_Page_List(CurrentPageIndex, PageSize, CountTotalRec, CurrentPageSection, command, BindDataItem)


}


function Asyncpager_ChangePageSize(ddlpagesize, PageSize, BindDataItem) {

    var _selectedpagesize = document.getElementById(ddlpagesize);
    document.getElementById(PageSize).value= _selectedpagesize.options[_selectedpagesize.selectedIndex].value;

    BindDataItem();
}