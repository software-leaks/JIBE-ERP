<%@ Page Title=" Jibe: FAQ" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LMS_Module_topic.aspx.cs" Inherits="LMS_LMS_Module_topic" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cplexdertask" %>
<%@ Register Src="../UserControl/ucAsyncPager.ascx" TagName="ucAsyncPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/Common.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui.min.js"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/FAQ_ModuleList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/jsasyncpager.js" type="text/javascript"></script>
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
            width: 99.65%;
            float: left;
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
            border-color:#BBCFCF;
            border-style:solid;
            border-radius: 25px;
            background-color:#BBCFCF;
            width:200px;
            margin-bottom:10px;
        }
       .FAQ-Video
        {
            color:#627A62;
            background-color:#D9DFD9;
            font-weight: bold;
            padding:5px 5px 5px;
        }
        .watermarked1
        {
            background-color: #F0F8FF;
            color:Silver;
            font-size: 11px;
        }
    </style>
    <script type="text/javascript">
        $(function () {

            asyncBindFAQModuleList();

        });
    </script>
    <script type="text/javascript">
        $(function () {
            $(':text').bind('keydown', function (e) {
                //on keydown for all textboxes
                if (e.target.className != "txtSearch") {

                    if (e.keyCode == 13) { //if this is enter key

                        asyncBindFAQModuleSearchList();
                        e.preventDefault();
                        return false;
                    }
                    else
                        return true;
                }
                else
                    return true;
            });
        });
    </script>
    <script type="text/javascript">


        function Refresh() {
            document.getElementById('txtSearch').value = "";
        }
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
        <div id="Title" class="page-title" style="background-color: #fff;">
            Help/FAQ
        </div>
        <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
            color: Black; background-color: #fff; text-align: center">
            <table id="testTable" width="89.5%" cellpadding="1" style="margin-bottom: 10px; margin-left: 5%;
                margin-top: 2%">
                <tr>
                    <td align="center" style="background-color: #BBCFCF; padding: 15px 15px 15px 15px;
                        width: 70%; background-clip: padding-box; border-radius: 5px; color: #627A62;
                        font-weight: bold">
                        Search Text :
                        <asp:TextBox ID="txtSearch" runat="server" MaxLength="100" Height="20px" Width="75%" BackColor="#FFFFE6"></asp:TextBox>
                        <tlk4:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtSearch"
                            WatermarkText="What can we help you with? Try searching keywords. " WatermarkCssClass="watermarked1" />
                        <cplexdertask:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtSearch"
                            CompletionSetCount="10" MinimumPrefixLength="4" ServiceMethod="GetFAQDescList"
                            ContextKey="" CompletionInterval="200" ServicePath="~/JibeWebService.asmx" runat="server">
                        </cplexdertask:AutoCompleteExtender>
                        <%-- ><a href="http://site.com" style=" color:Gray; font-size:medium;text-decoration: none !important;">Page</a>--%>
                        <%--<asp:ImageButton ID="btnserch" runat="server" OnClientClick="asyncBindFAQModuleSearchList();"
                            ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />
                        <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/Refresh-icon.png"
                            OnClientClick="Refresh()" ToolTip="Refresh" />--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="Div2" style="display: none; text-align: left">
                            <a href="#" style="color: #627A62; font-size: medium; text-decoration: none !important;"
                                onclick="asyncBindFAQModuleSearchList()" title=""> << Go Back to Search Result</a>
                        </div>
                    </td>
                </tr>
            </table>
            <table style="width: 90%; text-align: center; margin-left: 5%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <div id="dvasyncFAQList">
                            loading....
                        </div>
                        <div id="dvasyncFAQSearchList" style="display: none;">
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="2">
                                        <div id="Div1">
                                            loading....
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 87%">
                                        <uc1:ucAsyncPager ID="ucAsyncPager1" runat="server" />
                                    </td>
                                    <td style="width: 13%; height: 100%">
                                        <asp:Button ID="Button1" BorderStyle="None" BackColor="#E0EBEB" Height="30px" OnClientClick="asyncBindFAQModuleList()"
                                            Text=" Browse Full FAQ >>" runat="server" />
                                        <%-- <a style="color:#627A62;font-size:12px;text-decoration: none !important;background-color:#E0EBEB; padding:5%" href="#"  onclick="javascript:asyncBindFAQModuleList();">Browse Full FAQ >></a>--%>
                                    </td>
                                </tr>
                            </table>
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
        </div>
    </div>
</asp:Content>
