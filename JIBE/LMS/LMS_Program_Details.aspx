<%@ Page Title="Training/Drill  Details" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="LMS_Program_Details.aspx.cs" Inherits="LMS_Program_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
   
    <style type="text/css">
        .tdh
        {
            font-size: 11px;
            text-align: right;
            padding: 0px 3px 0px 0px;
            width: 130px;
            height: 20px;
            vertical-align: top;
            font-weight: bold;
        }
        .tdr
        {
            font-size: 11px;
            text-align: right;           
            
            vertical-align: top;
            font-weight: bold;
            
        }
        .tdd
        {
            font-size: 11px;
            text-align: left;
            padding: 0px 2px 0px 3px;
            height: 20px;
            vertical-align: top;
            white-space:nowrap;
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
        }
    </style>
    <script type="text/javascript">

        function validate() {
            if (document.getElementById('<%=txtProgramName.ClientID%>').value.trim() == "") {
                alert("Training/Drill Name is required field!");
                return false;
            }

        }

        function alertm(msg) {
            if (msg == "dur") {
                alert("Duration Value is not Valid!");
            }
            if (msg == "sav") {
                alert("Training/Drill Details Saved!");
            }
        }

        function OnLoad() {

            var links = document.getElementById("<%=tvItemList.ClientID %>").getElementsByTagName("a");

            for (var i = 0; i < links.length; i++) {

                if (links[i].getAttribute("href").toString() != "#" && links[i].getAttribute("href").toString().search(/uploads/gi) > 0) {
                    links[i].setAttribute("onclick", "javascript:NodeClick(\"" + links[i].getAttribute("href") + "\",\"" + links[i].getAttribute("target") + "\")");
                    links[i].setAttribute("href", "#");
                    links[i].setAttribute("target", "");
                }
                else if (links[i].getAttribute("href").toString() != "#" && links[i].getAttribute("href").toString().search(/Chapter/gi) > 0) {

                    links[i].setAttribute("onclick", "javascript:Show_Chapter_Details(\"" + links[i].getAttribute("href") + "\")");
                    links[i].setAttribute("href", "#");


                }
            }
        }

        function LMS_Play_video(src, Item_name) {

            var extension = src.substring(src.lastIndexOf('.') + 1);
            var filename = src.substring(src.lastIndexOf('/') + 1);

            if (extension == "mp3" || extension == "m4a") {

                document.getElementById('vdPlayControl').src = src.toString();
                document.getElementById('vdPlayControl').play();
                document.getElementById('dvVideoPlayer').style.display = 'block';
                document.getElementById('spnPlayerTitle').innerHTML = Item_name;
            }
            else
                window.open("../uploads/TrainingItems/" + filename)

        }

        function CheckIsNumeric(Duration) {

            if (isNaN(Duration.value)) {
                alert("Duration should be numeric.")
                Duration.value = '';
                //                 document.getElementById('txtDuration').focus();
                return false;
            }
        }

        function LMS_Disable_Right_Click() {

            return false;

        }

        function Show_Chapter_Details(href) {

            if (href == null) {


                var program_id = document.getElementById("<%=hdfProgram_Id.ClientID %>").value;
                var program_desc = document.getElementById("<%=txtProgramName.ClientID %>").value;
                var ProgramCategory = document.getElementById("<%=hdfProgramCategory.ClientID %>").value;

                //                if (program_id !=null && program_id.toString().trim().length == 0) {
                //                    alert("Save the Program .");
                //                    return;
                //                }

                href = "LMS_Chapter_Details.aspx?Chapter_ID=&Program_ID=" + program_id + "&ProgramCategory=" + ProgramCategory

            }


            var btn = "<%=btnSearch.ClientID %>";

            OpenPopupWindowBtnID('POP__ChapterDetails', 'Chapter Details', href, 'popup', 840, 1000, null, null, false, false, true, false, btn); return false;


        }

        function NodeClick(href, itemname) {

            LMS_Play_video(href, itemname);

        }

        function closeWin() {
            myWindow.close();
        }

        function showDiv(dv) {
            if (dv) {
                $('#' + dv).show();
            }
        }

    </script>
    <script type="text/javascript">

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Training/Drill Details
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
        <asp:UpdatePanel ID="updMain" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Style="display: none" />
                <table style="width: 100%;">
                    <tr>
                        <td class="tdh">
                            Training/Drill Name <span style="color:Red">*</span>:
                        </td>
                        <td class="tdd">
                            <asp:TextBox ID="txtProgramName" Width="250px" runat="server" MaxLength="50"></asp:TextBox>
                           
                        </td>
                        <td class="tdh">
                            Duration (in Days) <span style="color:Red"></span>:
                        </td>
                        <td class="tdd">
                            <asp:TextBox ID="txtDuration" runat="server" MaxLength="6" Width="160px" onchange="CheckIsNumeric(this)"></asp:TextBox>
                        </td>
                        <td class="tdr">
                            <asp:RadioButton ID="rdbVideo" runat="Server" Text="Office Training" GroupName="Program"  style="white-space:nowrap" Checked="true"/>
                        </td>
                        <td class="tdr" style="text-align:left" >
                            <asp:RadioButton ID="rdbTraining" runat="Server" Text="Vessel Training" GroupName="Program" style="white-space:nowrap"/>
                        </td>
                        
                        <td style="text-align: right">
                            <asp:Button ID="btnSaveProgram" runat="server" Text="Save Training/Drill" Width="120px"
                                Height="24px"     ValidationGroup="additems" OnClientClick="return validate();"
                                OnClick="btnSaveProgram_Click" />
                           
                        </td>
                    </tr>
                    <tr>
                        <td class="tdh">
                            Description &nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td class="tdd">
                            <asp:TextBox ID="txtProgramDescription" TextMode="MultiLine" Width="250px" runat="server" MaxLength="200"></asp:TextBox>
                        </td>
                        <td class="tdh">
                            Category :
                        </td>
                        <td class="tdd">
                            <asp:DropDownList ID="ddlProgramCategory" runat="server" BackColor="#FFFFCC" Font-Size="11px"
                                Height="20px" Visible="true" Width="160px" AutoPostBack="true"
                                 >
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table border="1" style="height: 100%" width="100%">
                    <tr>
                        <td style="width: 300px; vertical-align: top" height="700px">
                        <table style="width:100%">
                        <tr>
                        <td style="vertical-align:top">
                         <asp:TreeView ID="tvItemList" runat="server" Style="margin-right: 1px" BorderColor="#F3F1CD"
                                Font-Bold="False" Font-Names="Arial" Font-Size="Small" ForeColor="Black" EnableViewState="false"
                                ImageSet="XPFileExplorer" NodeIndent="15" AutoGenerateDataBindings="False" ShowLines="True">
                                <ParentNodeStyle Font-Bold="False" />
                                <HoverNodeStyle Font-Underline="False" ForeColor="#6666AA" BackColor="#99FF66" />
                                <SelectedNodeStyle Font-Underline="False" ForeColor="#6666AA" BackColor="#99FF66" />
                                <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                                    NodeSpacing="0px" VerticalPadding="2px" />
                            </asp:TreeView>
                        </td>
                        <td style="vertical-align:top;text-align:right">
                         <asp:Button ID="btnNewChapter" runat="server" Text="Add New Chapter"  OnClientClick="validate()"
                                Width="120px"  ValidationGroup="additems"
                                  Height="24px"   OnClick="btnNewChapter_Click" />
                        </td>
                        </tr>
                        </table>
                           
                            
                        </td>
                        <td style="width: 805px">
                            <div id="dvVideoPlayer" style="border: 1px solid gray; display: none">
                                <video id="vdPlayControl" controls='controls' style="border: 1px solid gray" height="700px"
                                    oncontextmenu="return LMS_Disable_Right_Click()" width="905px" src='../uploads/TrainingItems/dec768c4-393a-41c0-a6a3-c21ab2557854.mp4'>
                                </video>
                                <br />
                                <span id="spnPlayerTitle" style="color: Navy; font-size: 16px; margin: 10px"></span>
                            </div>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hdfProgram_Id" runat="server" />
                <asp:HiddenField ID="hdfChapter_Id" runat="server" />
                <asp:HiddenField ID="hdfProgramCategory" runat="server" />

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
