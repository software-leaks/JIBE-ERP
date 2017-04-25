<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" EnableEventValidation="false" Title="Permissions"
    CodeFile="PURC_Permissions.aspx.cs" Inherits="Purchase_PURC_Permissions" %>
    
<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"    TagPrefix="ucDDL" %>
<%@ Register TagPrefix="RJS" Namespace="RJS.Web.WebControl" Assembly="RJS.Web.WebControl.PopCalendar.Net.2008" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="header" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.4.4.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui.min.js"></script>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="JS/common.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
     <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            font-family: Tahoma;
            font-size: 11px;
        }
        .fileBrowser
        {
            font-size: 11px;
            height: 22px;
        }
        .fileBrowser input
        {
            width: 300px;
        }
        .xMessage
        {
            position: absolute;
            top: 2px;
            left: 200px;
            width: 400px;
            border: 1px solid outset;
            padding: 5px;
            background-color: #BEF781;
            font-weight: bold;
        }
        .bckColor
        {
            background-color: #F2F5A9;
        }
        .noborder
        {
            border: 0px;
            height: 17px;
            width: 200px;
        }
    </style>
    <script type="text/javascript">

        function Validation() {

            var FolderName = document.getElementById("txtNewFolderName").value.trim();

            if (FolderName == "") {
                alert("Folder name cannot be blank.");
                return false;
            }
            return true;
        }


        function SaveConfirmation() {
            var resp = window.confirm('File is already exists. do you want create another version?');
            if (resp == true) {

                document.getElementById('hdnVesionChecked').value = resp;
                window.form1.submit();
            }
        }

        function refreshParent() {

        }

        function AddCategory() {
            showModal('udpRACategory', true, closeHistory);
            document.getElementById("udpRACategory").title = "Add Category"

            return false;
        }

        //This is validation function to restrict special characters on save
        function specialcharecter() {

            if (document.getElementById($('[id$=FileUploader]').attr('id')) != null) {
                var iChars = "!`#$%^&*()+=[]\\\';,/{}|\":<>~_";
                var data = document.getElementById($('[id$=FileUploader]').attr('id')).value;

                var fullPath = document.getElementById($('[id$=FileUploader]').attr('id')).value;
                if (fullPath) {
                    var startIndex = (fullPath.indexOf('\\') >= 0 ? fullPath.lastIndexOf('\\') : fullPath.lastIndexOf('/'));
                    var filename = fullPath.substring(startIndex);
                    if (filename.indexOf('\\') === 0 || filename.indexOf('/') === 0) {
                        filename = filename.substring(1);
                    }
                }
                for (var i = 0; i < filename.length; i++) {
                    if (iChars.indexOf(filename.charAt(i)) != -1) {
                        alert("File name with special characters is not allowed.");

                        return false;
                    }
                }
            }
            return true;
        }
        function ValidationOnDate() {
            if (document.getElementById("txtFromDate").value != "") {
                var strFromDate = document.getElementById("txtFromDate").value;
                if (strFromDate != "") {
                    var currDate = new Date();
                    var strCurrentDt = currDate.format("dd-MM-yyyy");
                    var dt1 = parseInt(strCurrentDt.substring(0, 2), 10);
                    var mon1 = parseInt(strCurrentDt.substring(3, 5), 10);
                    var yr1 = parseInt(strCurrentDt.substring(6, 10), 10);

                    var dt2 = parseInt(strFromDate.substring(0, 2), 10);
                    var mon2 = parseInt(strFromDate.substring(3, 5), 10);
                    var yr2 = parseInt(strFromDate.substring(6, 10), 10);

                    var CurrentDt = new Date(yr1, mon1, dt1);
                    var FromDate = new Date(yr2, mon2, dt2);
                    if (FromDate == 'Invalid Date') {
                        alert('Invalid From Date.');
                        return false;
                    }
                }
            }
            if (document.getElementById("txtToDate").value != "") {
                var strToDate = document.getElementById("txtToDate").value;
                if (strToDate != "") {

                    var currDate = new Date();
                    var strCurrentDt = currDate.format("dd-MM-yyyy");
                    var dt1 = parseInt(strCurrentDt.substring(0, 2), 10);
                    var mon1 = parseInt(strCurrentDt.substring(3, 5), 10);
                    var yr1 = parseInt(strCurrentDt.substring(6, 10), 10);

                    var dt2 = parseInt(strToDate.substring(0, 2), 10);
                    var mon2 = parseInt(strToDate.substring(3, 5), 10);
                    var yr2 = parseInt(strToDate.substring(6, 10), 10);

                    var CurrentDt = new Date(yr1, mon1, dt1);
                    var ToDate = new Date(yr2, mon2, dt2);

                    if (ToDate == 'Invalid Date') {
                        alert('Invalid To Date.');
                        return false;
                    }

                }
            }
            return true;
        }

        function searchKeyPressDate(e) {
            // look for window.event in case event isn't passed in
            if (e.keyCode == 13) {
                document.getElementById('btnExecute').click();
                return false;
            }
            return true;
        }
      
    </script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $("#tabs").tabs();
        });
    </script>
    <script language="javascript" type="text/javascript">
        var pageInstance = Sys.WebForms.PageRequestManager.getInstance();
        pageInstance.add_pageLoaded(UpdateLabelHandler);

        function UpdateLabelHandler(sender, args) {
            var ControldataItems = args.get_dataItems();
            if ($get('lblMessage') !== null && ControldataItems['lblMessage'] != null)
                $get('lblMessage').innerHTML = ControldataItems['lblMessage'];
        }
    </script>
    <%--1.Maintain scroll position of a department checklistbox within a page on postback.--%>
    <script type="text/javascript">
        var xPos, yPos, xPos1, yPos1, xPosfolder, yPosfolder, xPos1folder, yPos1folder;

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);

        function BeginRequestHandler(sender, args) {
            // Edit Folder
            // save department's div scroll poisition
            if ($get('dvScroll') != null) {
                xPos = $get('dvScroll').scrollLeft;
                yPos = $get('dvScroll').scrollTop;
            }
            // save users's div scroll poisition
            if ($get('dvUsersScroll') != null) {
                xPos1 = $get('dvUsersScroll').scrollLeft;
                yPos1 = $get('dvUsersScroll').scrollTop;
            }

            // Add Folder
            if ($get('dvScrollDeptFolder') != null) {
                xPosfolder = $get('dvScrollDeptFolder').scrollLeft;
                yPosfolder = $get('dvScrollDeptFolder').scrollTop;
            }

            if ($get('dvScrollUserfolder') != null) {
                xPos1folder = $get('dvScrollUserfolder').scrollLeft;
                yPos1folder = $get('dvScrollUserfolder').scrollTop;
            }
        }

        function EndRequestHandler(sender, args) {
            // ratain department's div scroll poisition
            if ($get('dvScroll') != null) {
                $get('dvScroll').scrollLeft = xPos;
                $get('dvScroll').scrollTop = yPos;
            }

            // ratain user's div scroll poisition
            if ($get('dvUsersScroll') != null) {
                $get('dvUsersScroll').scrollLeft = xPos1;
                $get('dvUsersScroll').scrollTop = yPos1;
            }

            if ($get('dvScrollDeptFolder') != null) {
                $get('dvScrollDeptFolder').scrollLeft = xPosfolder;
                $get('dvScrollDeptFolder').scrollTop = yPosfolder;
            }

            if ($get('dvScrollUserfolder') != null) {
                $get('dvScrollUserfolder').scrollLeft = xPos1folder;
                $get('dvScrollUserfolder').scrollTop = yPos1folder;
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div style="background-color: Yellow; font-size: 12px; color: Red; text-align: center;">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </div>
                <div id="fragment2" runat="server" style="padding: 0px; display: inline">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div style="border: 1px solid #BDBDBD; padding: 10px; text-align: left;">
                                <asp:Panel ID="pnlEditFolder" runat="server">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table cellpadding="2" cellspacing="0" style="border: 1px solid #dcdcdc; width: 100%;">
                                            
                                                <tr>
                                                    <td align="center" valign="top" colspan="3">
                                                        <div class="page-title">
                                                            <asp:Label ID="Label2" runat="server" Text="Edit" Font-Bold="True" ForeColor="#003366"
                                                                ToolTip="Edit Permissions"></asp:Label></div>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td style="vertical-align: top; width: 130px;">
                                                        <asp:Label ID="lbldep" runat="server" Text="Function/Department/Stores"></asp:Label>
                                                    </td>
                                                    <td >
                                                      <%--  <asp:DropDownList ID="ddl_Function" runat="server" Width="150px" OnSelectedIndexChanged="ddl_DepartChange"
                                                            AutoPostBack="true">
                                                        </asp:DropDownList>--%>
                                                         <ucDDL:ucCustomDropDownList ID="ddl_Function" Style="float: left" runat="server" UseInHeader="false"
                                                        OnApplySearch="ddl_FunctionChange"  Height="200" Width="160" />
                                                        <%-- <asp:TextBox ID="txtParentFolderE" ReadOnly="true" runat="server" Width="320px"></asp:TextBox>--%>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top;">
                                                        <asp:Label ID="lbl_Catlog" runat="server" Text="System/Catalogue Name"></asp:Label>
                                                    </td>
                                                    <td>
                                                       <%-- <asp:DropDownList ID="ddl_Catlog" runat="server" Width="150px" AutoPostBack="true"
                                                            OnSelectedIndexChanged="Cataloge_Change" Enabled="false">
                                                        </asp:DropDownList>--%>
                                                        <ucDDL:ucCustomDropDownList ID="ddl_Catlog" Style="float: left" runat="server" UseInHeader="false"
                                                        OnApplySearch="Cataloge_Change" Height="200" Width="160" Enabled="false"/>
                                                        <%-- <asp:TextBox ID="txtFolderNameE" runat="server" Width="320px"></asp:TextBox>--%>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top;">
                                                        <asp:Label ID="lbl_SubCatlog" runat="server" Text="Sub-System/Sub-Catalogue Name"></asp:Label>
                                                    </td>
                                                    <td>
                                                       <%-- <asp:DropDownList ID="ddl_SubCatlog" runat="server" Width="150px" OnSelectedIndexChanged="OnsubCatlogChange" Enabled="false">
                                                        </asp:DropDownList>--%>
                                                          <ucDDL:ucCustomDropDownList ID="ddl_SubCatlog" Style="float: left" runat="server" UseInHeader="false"
                                                        OnApplySearch="OnsubCatlogChange" Height="200" Width="160" Enabled="false" />
                                                        <%-- <asp:TextBox ID="TextBox1" runat="server" Width="320px"></asp:TextBox>--%>
                                                    </td>
                                                    <caption>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </caption>
                                                    <caption>
                                                        
                                                    </caption>
                                                </tr>
                                                <tr>
                                            <td style="vertical-align: top; width: 130px;">
                                                        <asp:Label ID="Label1" runat="server" Text="Access Type"></asp:Label>
                                                    </td>
                                            <td> 
                                            <asp:RadioButtonList runat="server" ID="rbtAccessType" TextAlign="Left" RepeatDirection="Horizontal" OnSelectedIndexChanged="OnTypeChange" AutoPostBack="true"> 
                                            <asp:ListItem Text="Departmentwise" Value="Department" ></asp:ListItem>
                                            <asp:ListItem Text="Employeewise" Value="Employee" ></asp:ListItem>
                                            </asp:RadioButtonList>
                                            </td>
                                            </tr>
                                                <tr>
                                                    <td style="vertical-align: middle;">
                                                        Department Visibility Configuration
                                                    </td>
                                                    <td style="vertical-align: top">
                                                        <div id="dvScroll" style="max-height: 180px; height: 180px; width: 330px; overflow-y: auto;
                                                            background-color: White; border-color: Gray; border-style: solid; border-width: 1px">
                                                            <asp:CheckBoxList ID="chklstDept" runat="server" Width="300px" OnSelectedIndexChanged="chklstDept_SelectedIndexChanged"
                                                                AutoPostBack="true">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </td>
                                                    <td style="text-align: center; color: gray; font-size: 10px;">
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="TDUserlist" visible="false">
                                                    <td style="vertical-align: middle;">
                                                        User Visibility Configuration
                                                    </td>
                                                    <td style="vertical-align: top">
                                                        <div id="dvUsersScroll" style="max-height: 180px; overflow-y: auto; background-color: White;
                                                            border-color: Gray; border-style: solid; border-width: 1px; max-width: 330px">
                                                            <asp:CheckBoxList ID="chklstUser" runat="server" DataTextField="UserName" DataValueField="UserID"
                                                                OnSelectedIndexChanged="chklstUser_SelectedIndexChanged" AutoPostBack="true">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </td>
                                                    <td style="text-align: center; color: gray; font-size: 10px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <br />
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="text-align: center">
                                                        <asp:Button ID="btnSaveE" runat="server" Text="Save" OnClick="btnSaveE_Click" />
                                                        <asp:Button ID="Button1" runat="server" Text="Cancel" OnClientClick="javascript: setTimeout(window.close, 10);" />
                                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <asp:TextBox ID="txtNewFolderName" runat="server" Width="324px" Enabled="False" Visible="false"></asp:TextBox>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
