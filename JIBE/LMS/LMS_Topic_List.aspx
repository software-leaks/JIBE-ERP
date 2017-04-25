<%@ Page Title=" Jibe: FAQ" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LMS_Topic_List.aspx.cs" Inherits="LMS_LMS_Topic_List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cplexdertask" %>
<%@ Register Src="../UserControl/ucAsyncPager.ascx" TagName="ucAsyncPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/Common.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery.min.js"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui.min.js"></script>
    <script src="../Scripts/FAQ_ModuleList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/jsasyncpager.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../Styles/video-js.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/video.js" type="text/javascript"></script>
    <style type="text/css">
        .page
        {
            min-width: 400px;
            width: 99%;
        }
        .TableStyleCSS
        {
            width: 100%;
        }
        
        .QuestionCSS
        {
            font-size: x-large;
            font-weight: bold;
            color: White;
            background-color: #A8BABA;
            height: 50px;
            padding-left: 150px;
        }
        .QuestionCSS-Icon
        {
            width: 20px;
        }
        .QuestionCSS-FAQ
        {
            padding-top: 5px;
            padding-left: 50px;
            padding-bottom: 10px;
            background-color: #D9DFD9;
        }
        
        .QuestionCSS td
        {
            padding: 5px;
            padding-left: 50px;
        }
        .QuestionCSS-FAQ a
        {
            text-decoration: none;
            font-size: 12px;
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
            background-color: #E0EBEB;
            width: 99.86%;
            text-align: left;
            float: left;
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
            margin-bottom: 5px;
            list-style-type: disc;
            color: Gray;
            text-align: left;
        }
        td a:hover
        {
            background-color: #BBCFCF;
            font-weight: bold;
        }
        .TopicListCSS-show
        {
           list-style-type: disc;
            color: Gray;
            text-align: left;
            border-color:#BBCFCF;
            border-style:solid;
            border-radius: 25px;
            background-color:#BBCFCF;
            width:200px;
            margin-bottom:10px;
           
        }
        .AnswerCSS-Show-desc
        {
            display: block;
            color: #86868D;
            text-align: left;
            background-color: #E0EBEB;
            width: 99.65%;
            float: left;
            padding-top:15px;
            padding-bottom:15px;
          
        }
        .AnswerCSS-Show-FAQ
        {
            background-color: #E0EBEB;
            width:200px;
            height:100%;
           
        }
        .FAQ-Video
        {
            color:#627A62;
            background-color:#D9DFD9;
            font-weight: bold;
            padding:5px 5px 5px;
            margin-left:70px;
        }
        ul li:hover
        {
            background-color: #BBCFCF;
            cursor: pointer; 
            cursor: hand;
        }
        .abc
        {
            list-style-image: url("../images/right_arrow1.png"); 
            background-color:  #BBCFCF;
        }
    </style>
    <script type="text/javascript">
        $(function () {

            var Topic_ID = '<%=this.Request.QueryString["Topic_ID"]%>';
            GetTopicList(Topic_ID);

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField ID="hdf_User_ID" runat="server" />
   <div id="blur-on-updateprogress" style="display: none">
        <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
            color: black">
            <img src="../Images/loaderbar.gif" alt="Please Wait" />
        </div>
    </div>
    <div runat="server" id="ExportDiv">
        <div id="Title1" class="page-title" style="background-color: #fff;">
            Help/
        </div>
        <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
            color: Black; text-align: left; background-color: #fff; height: 90%">
            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <div id="dvasyncFAQQuestionList">
                            loading....
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="dvasyncFAQDescription" style="visibility: hidden;">
                            loading....
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="dvVideoPlayer" style="border: 1px solid gray; display: none;">
                            <video id="vdPlayControl" class="video-js vjs-default-skin" controls style="border: 1px solid gray"
                                height="700px" data-setup="{}" preload="none" oncontextmenu="return LMS_Disable_Right_Click()"
                                width="905px">
                                        <source src="" type="video/mp4">
                                    </video>
                            <br />
                            <span id="spnPlayerTitle" style="color: Navy; font-size: 16px; margin: 10px"></span>
                        </div>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="HdnValue" runat="server" />
            <asp:HiddenField ID="HdnCurrFAQ" runat="server" />
        </div>
    </div>
</asp:Content>
