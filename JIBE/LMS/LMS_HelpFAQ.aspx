<%@ Page Title="Help/FAQ" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LMS_HelpFAQ.aspx.cs" Inherits="LMS_LMS_HelpFAQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="../Scripts/jquery.min.js"></script>
    <link href="../Styles/Common.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/FAQ_ModuleList.js" type="text/javascript"></script>
    <link href="../Styles/video-js.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/video.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script type="text/javascript">
        videojs.options.flash.swf = "video-js.swf";
        function Play1(src, Item_name) {

            try {

                src = "../Uploads/TrainingItems/"+src
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
                document.getElementById('dvPlay').style.display = 'block';
                document.getElementById('dvVideoPlayer').style.display = 'block';
                document.getElementById('spnPlayerTitle').innerHTML = Item_name;
                document.getElementById('dvFAQDesc').style.display = "none";
                document.getElementById('dvasyncFAQDesc').style.display = "none";

            }
            catch (ex) {
                alert(ex.Message);
            }

        }
        function Play(src, Item_name) {

            try {


                var $vid_obj = _V_("vdPlayControl1");
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
                    $("#vdPlayControl1").removeClass("vjs-playing").addClass("vjs-paused");
                    // load the new sources
                    $vid_obj.load();
                    $("#div_video_html5_api").show();
                    $vid_obj.play();
                });
                

                document.getElementById('dvVideoPlayer1').title = Item_name;
                showModal('dvVideoPlayer1');
            }
            catch (ex) {
                alert(ex.Message);
            }

        }
        var lastExecutor = null;
        function Onfail() {
            alert("Not Successful!");
            document.getElementById('blur-on-updateprogress').style.display = 'none';
        }
        var __isResponse = 1;
        function Expand(FAQ_ID) {
            try {
                var _userid = $('[id$=HdnValue]').val();
                var _CompanyID = $('[id$=hdnCompanyID]').val();

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
            document.getElementById('dvFAQDesc').style.display = "block";
            document.getElementById('dvasyncFAQDesc').style.display = "block";
            document.getElementById('dvasyncFAQDesc').innerHTML = retVal.split("~totalrecordfound~")[0];
            document.getElementById('blur-on-updateprogress').style.display = 'none';
            document.getElementById('dvPlay').style.display = 'none';
            document.getElementById('dvVideoPlayer').style.display = 'none';
        }
    </script>
    <style type="text/css">
        .TableStyleCSS
        {
            width: 100%;
        }
        
        .ModuleCSS
        {
            background-color: #D9DFD9;
            height: 4%;
            text-align: left;
            padding-left: 1%;
            margin-right: 2%;
        }
        
        .CategoryCss
        {
            font-size: smaller;
            color: Gray;
            background-color: #E0EBEB;
        }
        .QuestionCSS-Icon
        {
            width: 20px;
        }
        .QuestionCSS-FAQ
        {
            font-weight: bold;
            color: Gray;
            font-size: x-large;
        }
        .QuestionCSS-FAQ a
        {
            font-weight: bold;
            text-decoration: none;
            font-size: 14px;
            color: Black;
        }
        .QuestionCSS td
        {
            padding: 3px;
            background-color: #dcdcdc;
            margin-bottom: 1px;
            border-bottom: 1px solid white;
        }
        
        
        .QuestionCSS-RecordInfo
        {
            padding: 5px;
            float: right;
        }
        .QuestionCSS-RecordInfo img
        {
            height: 16px;
            width: 16px;
            border-width: 0px;
            padding-left: 40;
        }
        .AnswerCSS-Hide
        {
            display: none;
        }
        .ShortAns-Hide
        {
            display: none;
        }
        .AnswerCSS-Show
        {
            display: block;
            color: #86868D;
            text-align: left;
            background-color: #E0EBEB;
        }
        .AnswerCSS-Show-desc
        {
            display: block;
            color: #86868D;
            text-align: left;
            background-color: #E0EBEB;
            width: 99.65%;
            float: left;
            padding-top: 15px;
            padding-bottom: 15px;
        }
        .AnswerCSS-Show-FAQ
        {
            background-color: #E0EBEB;
            width: 200px;
            height: 100%;
        }
        .AnsDiv
        {
            background-color: #efefef;
            border: 1px solid #cccccc;
            margin-left: 10px;
            padding: 5px;
        }
        
        .TopicListCSS
        {
            list-style-type: disc;
            color: Gray;
            text-align: left;
        }
        .watermarked
        {
            color: #CCCCCC;
        }
        a:hover
        {
            background-color: #E1E1E1;
        }
        .SpanCss
        {
            background-color: Gray;
            color: White;
            width: 20px;
            height: 20px;
            border-radius: 25px;
            text-align: center;
            float: left;
            font-weight: bold;
            font-size: large;
        }
        .TopicListCSS-show
        {
            list-style-type: disc;
            color: Gray;
            text-align: left;
            border-color: #BBCFCF;
            border-style: solid;
            border-radius: 25px;
            background-color: #BBCFCF;
            width: 200px;
            margin-bottom: 10px;
        }
        .FAQ-Video
        {
            color: #627A62;
            background-color: #D9DFD9;
            font-weight: bold;
            padding: 5px 5px 5px;
        }
        .tdrow
        {
            font-size: 11px;
            font-family: Tahoma;
            text-align: left;
            padding: 2px;
            vertical-align: top;
            line-height: 16px;
        }
        .tdtitle
        {
            font-weight: bold;
            color: Black;
            cursor: pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="blur-on-updateprogress" style="display: none">
        <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
            color: black">
            <img src="../Images/loaderbar.gif" alt="Please Wait" />
        </div>
    </div>
    <div runat="server" id="ExportDiv">
        <div id="Title" class="page-title" style="background-color: #fff;">
            Help/FAQ
        </div>
        <div id="dvFAQDesc" style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom;
            padding: 2px; color: Black; background-color: #fff; text-align: center; display: none">
            <table style="width: 98%; text-align: center; margin-left: 1%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <div id="dvasyncFAQDesc">
                            loading....
                        </div>
                        <asp:HiddenField ID="HdnValue" runat="server" />
                        <asp:HiddenField ID="hdnCompanyID" runat="server" />
                        <asp:HiddenField ID="hdnUserName" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="dvVideoPlayer1" style="border: 1px solid gray; display: none;">
                            <video id="vdPlayControl1" class="video-js vjs-default-skin" controls style="border: 1px solid gray"
                                height="700px" data-setup="{}" preload="none" oncontextmenu="return LMS_Disable_Right_Click()"
                                width="905px">
                                        <source src="" type="video/mp4">
                                    </video>
                            <br />
                            <span id="Span1" style="color: Navy; font-size: 16px; margin: 10px"></span>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="dvPlay" style="vertical-align: top; display:none;">
            <span id="spnPlayerTitle" style="color: Navy; font-size: 16px; margin: 10px;"></span>
            <table>
                <tr>
                    <td>
                        <div id="dvVideoPlayer" style="border: 1px solid gray; display: none; vertical-align: top">
                            <video id="vdPlayControl" class="video-js vjs-default-skin" controls style="border: 1px solid gray"
                                height="700px" data-setup="{}" preload="none" oncontextmenu="return LMS_Disable_Right_Click()"
                                width="905px">
                                                <source src="" type="video/mp4">
                                            </video>
                                            </div>
                         
                    </td>
                    <td valign="top" style="padding-left: 5px; background-color:#E0EBEB">
                        <b>Name:</b> <span class="tdrow ">
                            <%=this.Request.QueryString["Item_name"]%></span>
                        <br />
                        <br />
                        <b>Description:</b><span class="tdrow"><%=this.Request.QueryString["Item_Desc"]%></span>
                        <br />
                        <br />
                        <b>Duration:</b><span class="tdrow"><%=this.Request.QueryString["Duration"]%></span>
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </div>
</asp:Content>
