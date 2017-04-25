
var lastExecutor = null;
function Onfail() {

}



var __isResponse = 1;
function asyncBindFAQListFaq(_Edit) {
    try {
        
        __isResponse = 0;
        
        if (lastExecutor != null)
            lastExecutor.abort();

        var pagesize = document.getElementById('ctl00_MainContent_ucAsyncPager1_hdfPageSize').value;
        var pageindex = document.getElementById('ctl00_MainContent_ucAsyncPager1_hdfPageIndex').value;

        var _search = document.getElementById('txtSearch').value;
        if (document.getElementById('ctl00_MainContent_hdnEdit').value != "")
            _Edit = document.getElementById('ctl00_MainContent_hdnEdit').value;
        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_FAQ_List', false, { "SearchFaq": _search, "Page_Index": pageindex, "Page_Size": pagesize, "TableStyleCSS": "TableStyleCSS", "HeaderStyleCSS": "HeaderStyleCSS", "QuestionCSS": "QuestionCSS", "AnswerCSS": "AnswerCSS-Hide", "Edit": _Edit }, onSuccessasyncBindFAQListFaq, Onfail, new Array('t'));

        lastExecutor = service.get_executor();

    }
    catch (ex) {
        //document.getElementById('blur-on-updateprogress').style.display = 'block';
        alert(ex._message);
    }
}


function onSuccessasyncBindFAQListFaq(retVal, ev) {
    
    __isResponse = 1;
    document.getElementById('dvasyncFAQListFaq').innerHTML = retVal.split("~totalrecordfound~")[0];
    document.getElementById('ctl00_MainContent_ucAsyncPager1_hdfcountTotalRec').value = retVal.split("~totalrecordfound~")[1];
    Asyncpager_BuildPager(asyncBindFAQListFaq, 'ctl00_MainContent_ucAsyncPager1');
   
}

function toggle(faqId) {
    //alert(faqId);
    $('#' + faqId).toggleClass('AnswerCSS-Hide');
    $('#ShortAns' + faqId).toggleClass('ShortAns-Hide');
}
function EditFAQDetails(faq_id) {
    var myurl = 'LMS_FAQ_Builder.aspx?faq_id=' + faq_id;
    window.open(myurl);

//function EditFAQDetails(faq_id) {   
////    var strfaqid = faq_id;
////    var res = strfaqid.split(",");
//   //    document.getElementById("demo").innerHTML = res[1];
//        var myurl = 'LMS_FAQ_Builder.aspx?faq_id=' + faq_id;
////    var myurl = 'LMS_FAQ_Builder.aspx?faq_id='+res[0]+"&ATTACHMENT_ID="+res[1];
//    window.open(myurl);
}
