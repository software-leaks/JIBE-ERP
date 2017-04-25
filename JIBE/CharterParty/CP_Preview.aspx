<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CP_Preview.aspx.cs" Inherits="CharterParty_CP_Preview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script language="javascript" type="text/javascript">


            function DocOpen(docpath) {
                window.open(docpath);
            }

            function previewDocument(docPath) {
                document.getElementById("ifrmDocPreview").src = docPath;
            }
            function ValidationOnRetrieve() {
                //         debugger; 
                var cmdFleet = document.getElementById("ctl00_MainContent_DDLFleet").value;
                var cmdVessels = document.getElementById("ctl00_MainContent_DDLVessel").value;

                if (cmdFleet == "0" || cmdFleet == null) {
                    alert("Select fleet, vessel and click Retrieve button.");
                    return false;
                }

                if (cmdVessels == "ALL" || cmdVessels == null) {
                    alert("Select vessel and click Retrieve button.");
                    return false;
                }
                return true
            }

            function getImageopen(str) {
                window.open(str, "file", "menubar=0,resizable=0,width=750,height=550,resizeable=yes");
            }


            function OnAddAttachment() {
                var cmdFleet = document.getElementById("ctl00_MainContent_DDLFleet").value;
                var cmdVessels = document.getElementById("ctl00_MainContent_DDLVessel").value;



                if (cmdFleet == "0" || cmdFleet == null) {
                    alert("Select fleet, vessel and click Attach button.");
                    return false;
                }

                if (cmdVessels == "ALL" || cmdVessels == null) {
                    alert("Select vessel and click Attach button.");
                    return false;
                }


                return true;
            }

            function fn_OnClose() {
                $('[id$=btnLoadFiles]').trigger('click');
                //__doPostBack('ctl00_MainContent_btnLoadFiles', true);            
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 600px">
     <iframe ID="ifrmDocPreview" height="100%" marginheight="0px" 
                                                    src="../Images/previewAttach.png" 
                                                    style="vertical-align: middle; text-align: center;" width="100%"></iframe>
    </div>
    </form>
</body>
</html>
