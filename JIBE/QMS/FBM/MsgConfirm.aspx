<%@ Page Title="FBM Message Confirm" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="MsgConfirm.aspx.cs" Inherits="QMS_FBM_MsgConfirm" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <link href="../../Styles/jquery.alerts.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.alerts.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">


        function ValidationOnMakeActive() {

            $.alerts.okButton = " Yes ";
            $.alerts.cancelButton = " No ";


            var strMsg = "hi i m here "
                          + "Are you sure want to continue ?";

            var aa = jConfirm(strMsg, ' Confirmation Required !', function (r) {

                if (r) {

                    var postBackstr = "__doPostBack('" + id.replace(/_/gi, '$') + "','" + id.replace(/_/gi, '$') + "_Click')";
                    window.setTimeout(postBackstr, 0, 'JavaScript');
                    return true;


                }
                else {
                    return false;
                }
            }

            );

            return false;
        }
 


    </script>
    
    <style type="text/css">
        .ajax__htmleditor_editor_bottomtoolbar
        {
            display: none;
        }
        
        .cke_show_borders body
        {
            background: #FFFFCC;
            color: black;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function DocOpen(filename) {

            var filepath = "../../uploads/fbm/";
            //alert(filepath + filename);
            window.open(filepath + filename);
        }
        function ValidationOnSave() {


            var Department = document.getElementById("ctl00_MainContent_DDLOfficeDept").value;
            var PrimaryCategory = document.getElementById("ctl00_MainContent_DDLPrimaryCategory").value;
            var SecondryCategory = document.getElementById("ctl00_MainContent_DDLSecondryCategory").value;
            var Subject = document.getElementById("ctl00_MainContent_txtSubject").value;
            var eMailBody = document.getElementById("ctl00_MainContent_txtMailBody").value;


            if ((document.getElementById("ctl00_MainContent_optForUser_0").checked == false) && (document.getElementById("ctl00_MainContent_optForUser_1").checked == false) && (document.getElementById("ctl00_MainContent_optForUser_2").checked == false)) {
                alert('Please Select the For user option.');
                return false;
            }

            if (Department == "0") {
                alert('Department is required.')
                return false;
            }

            if (PrimaryCategory == "0") {
                alert('Primary Category is required.')
                return false;
            }


            if (SecondryCategory == "0") {
                alert('Secondry Category is required.')
                return false;
            }

            if (Subject == "") {
                alert('Subject is required.')
                return false;
            }

            return true;
        }

        function CloseDiv() {
            var control = document.getElementById("ctl00_MainContent_divSendApproval");
            alert('hi');
            control.style.visibility = "hidden";
        }

        function ValidationOnSendApproval() {

            var btnApproved = document.getElementById("ctl00_MainContent_ddlFbmApprover").value;
            if (btnApproved == "0") {
                alert('Pleas select the approver.')
                return false;
            }
        }




    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="border: 1px solid  #006699; font-family: Tahoma; font-size: 12px; color: Black;
            width: 100%; height: 860px">
            <div style="padding: 1px; background-color: #5588BB; color: #FFFFFF; text-align: center;">
                <b>
                    <asp:Label ID="lblTitle" Text="Fleet Broadcast Message-test page" runat="server"></asp:Label>
                </b>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <div>
                    

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
    </center>
    <div>
      
    </div>
</asp:Content>
