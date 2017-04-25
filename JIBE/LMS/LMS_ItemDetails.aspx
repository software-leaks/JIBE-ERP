<%@ Page Title="Add New Item" Language="C#" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="LMS_ItemDetails.aspx.cs" Inherits="LMS_ItemDetails" %>

<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="auc" %>
<%@ Register Assembly="AjaxControlToolkit4" Namespace="AjaxControlToolkit4" TagPrefix="tlk4" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <title></title>
    <style type="text/css">
        body
        {
            color: Black;
            font-family: Verdana;
            font-size: 11px;
            background-color: White;
        }
        ie
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
            text-align: left;
            padding: 2px;
            vertical-align: top;
            line-height: 16px;
        }
    </style>
    <script type="text/javascript">
      
        function fn_OnClose() {

        }
        function showDiv(dv) {
            if (dv) {
                $('#' + dv).show();
            }
        }
        function closeDiv(dv) {
            if (dv) {
                $('#' + dv).hide();
            }
        }

        function CheckIsNumeric(Duration) {
            if (isNaN(Duration.value)) {
                alert("Duration should be numeric.")
                Duration.value = '';
                return false;
            }
        }

        function Check_Duplicate_AttachmentFile(Attachment) {
            if (Attachment.value != '') {
                var retVal = confirm("Attached item already exist, Do you want to continue ?");
                if (retVal == false) {

                    return false;
                }

                else {
                    $(".csssaveitem").click();

                }

            }
        }

        function closeDiv() {
            parent.ReloadParent_ByButtonID();
        }

       
        function ClientUploaded(s, e) {
            document.getElementById('hiddenfieldid').value = s._filesInQueue.length;
            var size = Number(document.getElementById('hiddenSize').value)*1024;
            if (s._filesInQueue.length == 1) {
                document.getElementById("lblError").innerHTML = "";
                if ((e._fileSize) > size) {
                    alert("File size can not be more than " + size + " KB");
                    $(".cssFileSizebutton").click();

                }
                else {
                    s._filesInQueue[0]._status.innerHTML = "(Uploaded)";
                    document.getElementById("AjaxFileUpload1_SelectFileButton").style.display = "none";
                    document.getElementById("AjaxFileUpload1_SelectFileButton").innerHTML = "";
                    document.getElementById("AjaxFileUpload1_SelectFileButton").setAttribute('disabled', true);
                    document.getElementById("AjaxFileUpload1_Html5InputFile").style.display = "none";
                    document.getElementById("AjaxFileUpload1_UploadOrCancelButton").style.display = "none";
                    document.getElementById("AjaxFileUpload1_Html5DropZone").style.visibility="hidden";
                    document.getElementById("AjaxFileUpload1_Html5DropZone").innerHTML = "";
                }


            }
            else {
                document.getElementById("AjaxFileUpload1_Footer").style.display = "none";
                document.getElementById("AjaxFileUpload1_QueueContainer").style.display = "none";
                document.getElementById("lblError").innerHTML = "Only 1 File is allowed to Upload";
                document.getElementById("AjaxFileUpload1_QueueContainer").innerHTML = ""; 
               
            }
            

        }


      


    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptmngPO" runat="server">
    </asp:ScriptManager>
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
        <table style="width: 100%;">
            <tr>
                <td>
                    <div id="dvAddItem" style="display: block; border: 1px solid #A9BCF5; background-color: #EFF2FB;font-size:12px;
                        color: Black; padding: 10px;">
                        <table id="main" cellpadding="0" cellspacing="2" width="100%">
                            <tr>
                                <td class="tdh" style="font-size:12px;">
                                    Item Name<span style="color:Red">*</span>:
                                </td>
                                <td class="tdd" style="width: 250px">
                                    <asp:TextBox ID="txtItemName" MaxLength="250" Width="400px" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFD1" ControlToValidate="txtItemName" runat="server"
                                        ValidationGroup="additems" ErrorMessage="Required Field!"></asp:RequiredFieldValidator>
                                </td>
                                <td class="tdh"  style="font-size:12px;">
                                    Duration(in Minutes) :
                                </td>
                                <td class="tdd">
                                    <asp:TextBox ID="txtDuration" onchange="CheckIsNumeric(this)" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdh"  style="font-size:12px;">
                                    Description :
                                </td>
                                <td class="tdd">
                                    <asp:TextBox ID="txtDescription" TextMode="MultiLine" MaxLength="1000" Width="400px"  style="font-size:12px;"
                                        Height="80px" runat="server"></asp:TextBox>
                                </td>
                                <td class="tdh"  style="font-size:12px;"> 
                                    Item Type :
                                </td>
                                <td class="tdd">
                                    <asp:DropDownList ID="ddlItemType" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged" Width="100px" Height="20px"
                                        Font-Size="11px" BackColor="#FFFFCC">
                                    </asp:DropDownList>
                                </td>
                                <td class="tdd">
                                    <asp:DropDownList ID="ddlFBMNumber" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        Visible="false" DataTextField="FBM_Number" DataValueField="FBM_Number" OnSelectedIndexChanged="ddlFBMNumber_SelectedIndexChanged"
                                        Width="140px" Height="20px" Font-Size="11px" BackColor="#FFFFCC">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdh">
                                    <asp:Label ID="lblAttachment" runat="server"  style="font-size:12px;">Attachment<span style="color:Red">*</span>     :</asp:Label>
                                </td>
                                <td class="tdd" >
                                    <asp:UpdatePanel ID="updupload" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btnUploadclick" runat="server" CssClass="cssFileSizebutton" Style="display: none" />
                                            <asp:Label runat="server" ID="myThrobber" Style="display: none;"><img align="absmiddle" alt="" src="uploading.gif"/></asp:Label>
                                            <tlk4:AjaxFileUpload ID="AjaxFileUpload1" runat="server" Padding-Bottom="2" Padding-Left="2"
                                                OnClientUploadComplete="ClientUploaded" Height="80px" Width="400px" Padding-Right="1"
                                                Padding-Top="2" ThrobberID="myThrobber" OnUploadComplete="AjaxFileUpload1_OnUploadComplete" 
                                                MaximumNumberOfFiles="1"  />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </td>
                               <td class="tdh" style="font-size:12px;">
                                <asp:Label Text="" ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                            <%--    <asp:HiddenField id ="hiddenfieldid" runat="server" />--%>
                                <input type="hidden" id ="hiddenfieldid" runat="server" />
                                <input type="hidden" id="hiddenSize" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right" colspan="4">
                                    <asp:Button ID="btnSaveandAddNew" runat="server" Text=" Save & Add New " Width="120px"
                                        ValidationGroup="additems" OnClick="btnSaveItemDetails_Click" />
                                    <asp:Button ID="btnsaveandclose" runat="server" Text=" Save & Close " Width="120px"
                                        ValidationGroup="additems" OnClick="btnSaveItemDetails_Click"  />
                                    <asp:Button ID="btnClose" runat="server" Text=" Close " Width="70px" OnClientClick="closeDiv('dvAddItem'); return false;"
                                        ValidationGroup="additems" />
                                    <asp:Button ID="saveitem" runat="server" Style="display: none" Text="save" CssClass="csssaveitem"
                                        OnClick="saveitem_Click" />
                                </td>
                            </tr>
                        </table>
                </td>
            </tr>
        </table>
        <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
        <br />
    </div>
    </form>
</body>
</html>
