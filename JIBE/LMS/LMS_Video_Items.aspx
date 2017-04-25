<%@ Page Title="JiBe Training" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LMS_Video_Items.aspx.cs" Inherits="LMS_VideoItems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/video-js.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/video.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/FAQ_ModuleList.js" type="text/javascript"></script>
    <script type="text/javascript">
        videojs.options.flash.swf = "video-js.swf";
     
    </script>
    <style type="text/css">
        .tdh
        {
            font-size: 11px;
            text-align: right;
            padding: 0px 3px 0px 0px;
            width: 120px;
            height: 20px;
            vertical-align: middle;
            font-weight: bold;
        }
        .tdd
        {
            font-size: 11px;
            text-align: left;
            padding: 0px 2px 0px 3px;
            height: 20px;
            vertical-align: middle;
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
        .rowofvideo
        {
            vertical-align: top;
        }
    </style>
    <script type="text/javascript">
        var Video_ID = new Array();
        function LMS_Play_video(src, Item_name, Is_Helpful, ID) {
            try {


                //            while (src.charAt(0) == ".") src = href.substr(1);
                //            //            src = "https://player.vimeo.com/video" + src;
                src = "https://player.vimeo.com/video/158294752";


                //                var $vid_obj = _V_("vdPlayControl");
                //                // hide the current loaded poster
                //                $vid_obj.destr
                //                $("img.vjs-poster").hide();

                //                $vid_obj.ready(function () {
                //                    // hide the video UI
                //                    $("#div_video_html5_api").hide();
                //                    // and stop it from playing
                //                    $vid_obj.pause();
                //                    // assign the targeted videos to the source nodes
                //                    $("video:nth-child(1)").attr("src", src);

                //                    // reset the UI states
                //                    $(".vjs-big-play-button").hide();
                //                    $("#vdPlayControl").removeClass("vjs-playing").addClass("vjs-paused");
                //                    // load the new sources
                //                    $vid_obj.load();
                //                    $("#div_video_html5_api").show();
                //                    $vid_obj.play();
                //                });

                for (index = 0; index < Video_ID.length; index++) {
                    if (ID == Video_ID[index]) {
                        Is_Helpful = ID;
                    }
                }
                $("#vdPlayControl").attr("src", src);
                if (Is_Helpful == "") {
                    document.getElementById('Is_Helpful').style.display = 'block';
                    document.getElementById('ctl00_MainContent_HdnVideoID').value = ID;
                } else {
                    document.getElementById('Is_Helpful').style.display = 'none';
                }

                document.getElementById('dvVideoPlayer').style.display = 'block';
                document.getElementById('spnPlayerTitle').innerHTML = Item_name;

            }
            catch (ex) {
                alert(ex.Message);
            }

        }

        function Video_IsHelpful(str) {

            var _userid = $('[id$=HdnUserID]').val();
            var _video_ID = $('[id$=HdnVideoID]').val();
            if (str == "Yes") {
                var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'UpdateVideo_IsHelpful', false, { "Video_ID": _video_ID, "IsHelpful": 1, "UserID": _userid }, onSuccess, Onfail, new Array('t'));
            } else {
                var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'UpdateVideo_IsHelpful', false, { "Video_ID": _video_ID, "IsHelpful": 0, "UserID": _userid }, onSuccess, Onfail, new Array('t'));
            }

        }



        function onSuccess(retVal, ev) {
            // Video_ID[Video_ID.length] = retVal;
            document.getElementById('Is_Helpful').style.display = 'none';
        }

        function LMS_Disable_Right_Click() {

            return false;

        }



        function OnLoad() {

            var links = document.getElementById("<%=tvItemList.ClientID %>").getElementsByTagName("a");

            for (var i = 0; i < links.length; i++) {

                if (links[i].getAttribute("href").toString() != "#" && links[i].getAttribute("href").toString().search(/uploads/gi) > 0) {
                    links[i].setAttribute("onclick", "javascript:NodeClick(\"" + links[i].getAttribute("href") + "\",\"" + links[i].getAttribute("target") + "\")");
                    links[i].setAttribute("href", "#");
                    links[i].setAttribute("target", "");
                }
            }
        }

        function NodeClick(href, itemname) {
            var res = itemname.split(":");

            LMS_Play_video(href, res[0]);
            InsertVideoView(res[2], res[1], res[0]);

        }

        function InsertVideoView(userid, chaperid, itemname) {

            if (lastExecutor != null)
                lastExecutor.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../WebService.asmx', 'InsertVideoView', false, { "userid": userid, "chaperid": chaperid, "itemname": itemname }, OnSuccGet_InsertVideoView, Onfail, new Array(1, 1));
            lastExecutor = service.get_executor();
        }
        function OnSuccGet_InsertVideoView(retval, prm) {
            try {

            }
            catch (ex)
    { }
        }


        function Onfail(retval) {
            alert(retval);
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        JiBe Video Training
    </div>
    <div class="page-content-main">
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 40%; top: 30px; z-index: 2;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <%-- <asp:UpdatePanel ID="updMain" runat="server">
            <ContentTemplate>--%>
        <table>
            <tr>
                <td class="tdh">
                    Item Name:
                </td>
                <td class="tdd">
                    <asp:TextBox ID="txtSearchItemName" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSearchItem" runat="server" Text="Search" OnClick="btnSearchItem_Click" />
                </td>
            </tr>
        </table>
        <table border="0" style="width: 100%">
            <tr>
                <td style="width: 300px; vertical-align: top; border: 1px solid gray;">
                    <div style="text-align: center" id="treelist" runat="server">
                        <asp:TreeView ID="tvItemList" runat="server" Style="margin-right: 1px" BorderColor="#F3F1CD"
                            Font-Bold="False" Font-Names="Arial" Font-Size="Small" ForeColor="Black" EnableViewState="false"
                            ImageSet="XPFileExplorer" NodeIndent="15" AutoGenerateDataBindings="False">
                            <ParentNodeStyle Font-Bold="False" />
                            <HoverNodeStyle Font-Underline="False" ForeColor="#6666AA" BackColor="#99FF66" />
                            <SelectedNodeStyle Font-Underline="False" ForeColor="#6666AA" BackColor="#99FF66" />
                            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                                NodeSpacing="0px" VerticalPadding="2px" />
                        </asp:TreeView>
                    </div>
                    <div style="text-align: center">
                        <asp:Label ID="lblNrf" runat="server" Style="font-size: 12px; font-weight: bold;
                            color: Red; text-align: center" Text="No Records Found!" Visible="false"></asp:Label>
                        <br />
                        <br />
                    </div>
                </td>
                <td class="rowofvideo">
                    <div style="vertical-align: top;">
                        <span id="spnPlayerTitle" style="color: Navy; font-size: 16px; margin: 10px; vertical-align: top">
                        </span>
                        <div id="dvVideoPlayer" style="border: 1px solid gray; display: none; vertical-align: top">
                            <%-- <video id="vdPlayControl" class="video-js vjs-default-skin" controls style="border: 1px solid gray"
                                height="700px" data-setup="{}" preload="none" oncontextmenu="return LMS_Disable_Right_Click()"
                                width="905px">
                                        <source src="" type="video/mp4">
                                    </video>--%>
                            <iframe src="" style="border: 1px solid gray; height: 700px; width: 905px" frameborder="0"
                                id="vdPlayControl" webkitallowfullscreen mozallowfullscreen allowfullscreen>
                            </iframe>
                            <br />
                            <div id="Is_Helpful" style="display: none;">
                                Was this helpful?<a href="javascript:Video_IsHelpful('Yes');">Yes</a>/<a href="javascript:Video_IsHelpful('No');">No</a></div>
                        </div>
                        <div style="vertical-align: top;">
                            <asp:DataList ID="dtlItemList" runat="server" RepeatColumns="2" ItemStyle-VerticalAlign="Top"
                                RepeatDirection="Horizontal" CellSpacing="20" RepeatLayout="Table" Width="100%">
                                <FooterStyle ForeColor="Red" HorizontalAlign="Left" Font-Bold="true" Font-Size="12px" />
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 150px; vertical-align: top">
                                                <video preload="none" poster="../images/LMS_Poster.png" title="Click to play" height="80px"
                                                    width="100px" src='<%# "../uploads/TrainingItems/"+Eval("ITEM_PATH").ToString()%>'
                                                    onclick="LMS_Play_video('<%#  "../uploads/TrainingItems/"+Eval("ITEM_PATH").ToString()%>','<%#Eval("ITEM_NAME")%>','<%#Eval("Is_Helpful")%>','<%#Eval("ID")%>')"
                                                    oncontextmenu="return LMS_Disable_Right_Click()">
                                                        </video>
                                            </td>
                                            <td class="tdrow" style="width: 70%">
                                                <span class="tdrow tdtitle" onclick="LMS_Play_video('<%# "../uploads/TrainingItems/"+Eval("ITEM_PATH").ToString()%>','<%#Eval("ITEM_NAME")%>','<%#Eval("Is_Helpful")%>','<%#Eval("ID")%>')">
                                                    <%#Eval("ITEM_NAME")%>
                                                </span>
                                                <br />
                                                <span class="tdrow">
                                                    <%#Eval("ITEM_Description")%>
                                                </span>
                                                <br />
                                                <span class="tdrow">
                                                    <%#Eval("DURATION")%>
                                                </span>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <%--  <FooterTemplate>
                            <asp:Label Visible='<%#bool.Parse((tvItemList.Nodes.Count==0).ToString())%>' runat="server" ID="lblNoRecord" Text="No Records Found!"></asp:Label>
                        </FooterTemplate>--%>
                            </asp:DataList>
                            <auc:CustomPager ID="ucCustomPagerAllStatus" OnBindDataItem="BindTrainingItems" PageSize="10"
                                AlwaysGetRecordsCount="true" runat="server" />
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HdnUserID" runat="server" />
        <asp:HiddenField ID="HdnVideoID" runat="server" />
        <%--    </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
