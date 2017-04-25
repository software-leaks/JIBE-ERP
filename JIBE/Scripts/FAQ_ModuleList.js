var lastExecutor = null;
function Onfail() {
    alert("Not Successful!");
    document.getElementById('blur-on-updateprogress').style.display = 'none';
}



var __isResponse = 1;
function asyncBindFAQModuleList() {
    try {
       
        __isResponse = 0;
        setTimeout(function () { if (__isResponse == 0) { document.getElementById('blur-on-updateprogress').style.display = 'block'; } }, 20);
        if (lastExecutor != null)
            lastExecutor.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_FAQ_ModuleList', false, { "TableStyleCSS": "TableStyleCSS", "ModuleCSS": "ModuleCSS", "TopicListCSS": "TopicListCSS" }, onSuccessasyncBindFAQModuleList, Onfail, new Array('t'));

        lastExecutor = service.get_executor();

    }
    catch (ex) {
       
        alert(ex._message);
    }
}


function onSuccessasyncBindFAQModuleList(retVal, ev) {
    __isResponse = 1;
    document.getElementById('dvasyncFAQList').innerHTML = retVal;
    document.getElementById('Div2').style.display = "none";
    document.getElementById('blur-on-updateprogress').style.display = 'none';
}


function GetTopicList(Topic_ID) {
    try {

        __isResponse = 0;
        var _userid = $('[id$=HdnValue]').val();
        setTimeout(function () { if (__isResponse == 0) { document.getElementById('blur-on-updateprogress').style.display = 'block'; } }, 20);
        if (lastExecutor != null)
            lastExecutor.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_Topic_FAQList', false, { "Topic_ID": Topic_ID, "FAQ_ID": 0, "Description": "", "TableStyleCSS": "TableStyleCSS", "QuestionCSS": "QuestionCSS", "AnswerCSS": "AnswerCSS-Show", "TopicListCSS": "TopicListCSS", "CategoryCss": "CategoryCss", "Page_Index": 0, "Page_Size": 0, "UserID": _userid }, onSuccessasyncBindFAQList, Onfail, new Array('t'));

        lastExecutor = service.get_executor();

    }
    catch (ex) {
      
        alert(ex._message);
    }
   
}

function onSuccessasyncBindFAQList(retVal, ev) {
    __isResponse = 1;

    document.getElementById('Title1').innerHTML = retVal.split("~totalrecordfound~")[0];
    document.getElementById('dvasyncFAQQuestionList').innerHTML = retVal.split("~totalrecordfound~")[1];
    document.getElementById('blur-on-updateprogress').style.display = 'none';

}

function asyncBindFAQModuleSearchList() {

    try {
        
        __isResponse = 0;
         var _userid = $('[id$=HdnValue]').val();
        var pagesize = document.getElementById('ctl00_MainContent_ucAsyncPager1_hdfPageSize').value;
        var pageindex = document.getElementById('ctl00_MainContent_ucAsyncPager1_hdfPageIndex').value;
        var _search = $('[id$=txtSearch]').val();

        if (_search != '') {
            setTimeout(function () { if (__isResponse == 0) { document.getElementById('blur-on-updateprogress').style.display = 'block'; } }, 20);
            if (lastExecutor != null)
                lastExecutor.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_Topic_FAQList', false, { "Topic_ID": 0, "FAQ_ID": 0, "Description": _search, "TableStyleCSS": "TableStyleCSS", "QuestionCSS": "QuestionCSS", "AnswerCSS": "AnswerCSS-Show", "TopicListCSS": "TopicListCSS", "CategoryCss": "CategoryCss", "Page_Index": pageindex, "Page_Size": pagesize, "UserID": _userid }, onSuccessasyncBindFAQModuleSearchList, Onfail, new Array('t'));

            lastExecutor = service.get_executor();
        }
    }
    catch (ex) {
       
        alert(ex._message);
    }

}

function onSuccessasyncBindFAQModuleSearchList(retVal, ev) {
    __isResponse = 1;
        document.getElementById('Div2').style.display = "none"; 
        document.getElementById('dvasyncFAQList').style.display = "none"; 
        document.getElementById('dvasyncFAQSearchList').style.display = "block";
        document.getElementById('Div1').innerHTML = retVal.split("~totalrecordfound~")[0];
        document.getElementById('ctl00_MainContent_ucAsyncPager1_hdfcountTotalRec').value = retVal.split("~totalrecordfound~")[1];
      
        Asyncpager_BuildPager(asyncBindFAQModuleSearchList, 'ctl00_MainContent_ucAsyncPager1');
        document.getElementById('blur-on-updateprogress').style.display = 'none';
    }

function Expand(FAQ_ID) {
    try {
        var _userid = $('[id$=HdnValue]').val();
        setTimeout(function () { if (__isResponse == 0) { document.getElementById('blur-on-updateprogress').style.display = 'block'; } }, 20);
        if (lastExecutor != null)
            lastExecutor.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_Topic_FAQList', false, { "Topic_ID": 0, "FAQ_ID": FAQ_ID, "Description": "", "TableStyleCSS": "TableStyleCSS", "QuestionCSS": "QuestionCSS", "AnswerCSS": "AnswerCSS-Show", "TopicListCSS": "TopicListCSS", "CategoryCss": "CategoryCss", "Page_Index": 0, "Page_Size": 0, "UserID": _userid }, onSuccessSearchList, Onfail, new Array('t'));

        lastExecutor = service.get_executor();
    }
    catch (ex) {
   
        alert(ex._message);
    }
}

function onSuccessSearchList(retVal, ev) {
    __isResponse = 1;
   
            document.getElementById('dvasyncFAQList').style.display = "block";
            document.getElementById('dvasyncFAQSearchList').style.display = "none";
           document.getElementById('dvasyncFAQList').innerHTML = retVal.split("~totalrecordfound~")[0];
           document.getElementById('Div2').style.display = "block";
           document.getElementById('blur-on-updateprogress').style.display = 'none';
}

function Description(FAQ_ID) {
    try {
        var id = 'ID' + FAQ_ID;
        var CurrFAQ = $('[id$=HdnCurrFAQ]').val();
        if (CurrFAQ != "") {
            $('[id$=' + CurrFAQ + ']').removeClass("abc");
        }

        $('[id$=' + id + ']').addClass("abc");

        $('[id$=HdnCurrFAQ]').val(id);

        var _userid = $('[id$=HdnValue]').val();
        setTimeout(function () { if (__isResponse == 0) { document.getElementById('blur-on-updateprogress').style.display = 'block'; } }, 20);
        if (lastExecutor != null)
            lastExecutor.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_Topic_FAQList', false, { "Topic_ID": 0, "FAQ_ID": FAQ_ID, "Description": "", "TableStyleCSS": "TableStyleCSS", "QuestionCSS": "QuestionCSS", "AnswerCSS": "AnswerCSS-Show", "TopicListCSS": "TopicListCSS", "CategoryCss": "CategoryCss", "Page_Index": 0, "Page_Size": 0, "UserID": _userid }, onSuccessasyncBindFAQList1, Onfail, new Array('t'));

        lastExecutor = service.get_executor();

    }
    catch (ex) {
        
        alert(ex._message);
    }
}

function onSuccessasyncBindFAQList1(retVal, ev) {

__isResponse = 1;
document.getElementById('dvasyncFAQDescription').style.visibility = "visible";
document.getElementById('dvasyncFAQDescription').innerHTML = retVal.split("~totalrecordfound~")[0];
document.getElementById('blur-on-updateprogress').style.display = 'none';
}

function IsHelpful(str,FAQ_ID) {

    var _userid = $('[id$=HdnValue]').val();
    setTimeout(function () { if (__isResponse == 0) { document.getElementById('blur-on-updateprogress').style.display = 'block'; } }, 20);
    if (str == "Yes") {
        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'UpdateIsHelpful', false, { "FAQ_ID": FAQ_ID, "IsHelpful": 1, "UserID": _userid }, onSuccess, Onfail, new Array('t'));
    } else {
        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'UpdateIsHelpful', false, { "FAQ_ID": FAQ_ID, "IsHelpful": 0, "UserID": _userid }, onSuccess, Onfail, new Array('t'));
    }

}


function onSuccess(retVal, ev) {
   
    if (document.getElementById('dvasyncFAQList') != null) {
        Expand(retVal);
    } else {
        Description(retVal);
    }
}

function Play(src, Item_name) {
   
        try {
            src = "../uploads/TrainingItems/" + src;
            var $vid_obj = _V_("vdPlayControl");
            // hide the current loaded poster
            $("img.vjs-poster").hide();

            $vid_obj.ready(function () {
                // hide the video UI
                $("#div_video_html5_api").hide();
                // and stop it from playing
                $vid_obj.pause();
                // assign the targeted videos to the source nodes
                $("video:nth-child(1)").attr("src", src);

                // reset the UI states
                $(".vjs-big-play-button").hide();
                $("#vdPlayControl").removeClass("vjs-playing").addClass("vjs-paused");
                // load the new sources
                $vid_obj.load();
                $("#div_video_html5_api").show();
                $vid_obj.play();
            });
            document.getElementById('dvVideoPlayer').title = Item_name;
            showModal('dvVideoPlayer');


        }
        catch (ex) {
            alert(ex.Message);
        }

    }


    function LMS_Disable_Right_Click() {

        return false;

    }


function onSuccessLink(retVal, ev) {
    document.getElementById('blur-on-updateprogress').style.display = 'none';
}
