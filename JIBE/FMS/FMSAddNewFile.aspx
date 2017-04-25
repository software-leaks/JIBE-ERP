<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FMSAddNewFile.aspx.cs" Inherits="AddNewFile"
    MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Add File Form</title>
    <script type="text/javascript" src="../Scripts/jquery-1.4.4.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui.min.js"></script>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="JS/common.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
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
</head>
<body style="margin: 0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
    <div style="background-color: Yellow; font-size: 12px; color: Red; text-align: center;">
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </div>
    <div id="tabs" style="margin-top: 0px; width: 99%; display: block;">
        <ul>
            <li><a href="#fragment1" id="AddNewFile1" runat="server"><span>Add New File/Folder</span></a></li>
            <li><a href="#fragment2" id="EditNewFile" runat="server"><span>Edit Folder</span></a></li>
            <li><a href="#fragment3" id="SearchDocument" runat="server"><span>Documents Search</span></a></li>
            <li><a href="#fragment4" id="ScheduleInfo" runat="server"><span>Schedule Info</span></a></li>
        </ul>
        <div id="fragment1" runat="server" style="padding: 0px; display: inline">
            <div style="border: 1px solid #BDBDBD; padding: 10px; text-align: left;">
                <asp:Panel ID="pnlUploadDocument" runat="server">
                    <table cellpadding="2" cellspacing="0" style="border: 1px solid #dcdcdc; width: 100%;">
                        <tr>
                            <td align="center" valign="top" colspan="3">
                                <asp:Label ID="lblFormTitle" runat="server" Text="Add New File/Folder" Font-Bold="True"
                                    ForeColor="#003366" ToolTip="Add New File/Folder"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 130px;">
                                <asp:Label ID="lblDocName" runat="server" Text="Parent Folder"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFolderName" ReadOnly="true" runat="server" Width="320px"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                <asp:Label ID="lblDocumentType" runat="server" Text="Document Type"></asp:Label>
                            </td>
                            <td style="vertical-align: top">
                                <asp:RadioButtonList ID="rdoDocumentType" runat="server" Width="324px" CssClass="bckColor"
                                    AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdoDocumentType_SelectedIndexChanged">
                                    <asp:ListItem id="rdoBtnFile" runat="server" Value="FILE" Text="File" Selected="True" />
                                    <asp:ListItem id="rdoBtnFolder" runat="server" Value="FOLDER" Text="Folder" />
                                </asp:RadioButtonList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <asp:Panel ID="pnlDeptFolder" runat="server" Visible="false">
                            <asp:UpdatePanel ID="update1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <tr>
                                        <td style="vertical-align: top;">
                                            <asp:Label ID="lblFolderName" runat="server" Text="New Folder Name" Enabled="False"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNewFolderName" runat="server" Width="324px" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">
                                            Department Visibility Configuration
                                        </td>
                                        <td style="vertical-align: top">
                                            <div id="dvScrollDeptFolder" style="max-height: 180px; height: 180px; width: 330px;
                                                overflow-y: auto; background-color: White; border-color: Gray; border-style: solid;
                                                border-width: 1px">
                                                <asp:CheckBoxList ID="lstDept" runat="server" Width="300px" OnSelectedIndexChanged="lstDept_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:CheckBoxList>
                                            </div>
                                        </td>
                                        <td style="text-align: center; color: gray; font-size: 10px;">
                                            <%-- Click + Drag Down to select multiple Department<br />
                                            <br />
                                            OR<br />
                                            Hold Ctrl Key + Click on Department Name to select/un-select multiple Department--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">
                                            User Visibility Configuration
                                        </td>
                                        <td style="vertical-align: top">
                                            <div id="dvScrollUserfolder" style="max-height: 180px; height: 180px; width: 330px;
                                                overflow-y: auto; background-color: White; border-color: Gray; border-style: solid;
                                                border-width: 1px">
                                                <asp:CheckBoxList ID="lstUser" runat="server" Width="300px" DataTextField="UserName"
                                                    DataValueField="UserID" OnSelectedIndexChanged="lstUser_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:CheckBoxList>
                                            </div>
                                        </td>
                                        <td style="text-align: center; color: gray; font-size: 10px;">
                                            <%--  Click + Drag Down to select multiple Department<br />
                                            <br />
                                            OR<br />
                                            Hold Ctrl Key + Click on Department Name to select/un-select multiple Department--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; width: 100px;">
                                            <asp:Label ID="lblApproval" runat="server" Text="Approval Required" Height="5px"></asp:Label><br />
                                            <br />
                                            <br />
                                            <asp:Label ID="lblLevel1" runat="server" Text="Level1:"></asp:Label><br />
                                            <br />
                                            <asp:Label ID="lblLevel2" runat="server" Text="Level2:"></asp:Label><br />
                                            <br />
                                            <asp:Label ID="lblLevel3" runat="server" Text="Level3:"></asp:Label><br />
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:CheckBox ID="chkReqApproval" runat="server" CssClass="bckColor" AutoPostBack="true"
                                                OnCheckedChanged="chkReqApproval_OnCheckedChanged" /><br />
                                            <br />
                                            <asp:DropDownList ID="ddlLevel1" AppendDataBoundItems="true" runat="server" CssClass="txtInput"
                                                AutoPostBack="true" Width="200px" Enabled="false" DataSourceID="ObjectDataSource_Users"
                                                DataTextField="USERNAME" DataValueField="USERID" OnSelectedIndexChanged="ddlLevel1_OnSelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            <br />
                                            <asp:DropDownList ID="ddlLevel2" AppendDataBoundItems="true" runat="server" CssClass="txtInput"
                                                AutoPostBack="true" Width="200px" Enabled="false" DataSourceID="ObjectDataSource_Users"
                                                DataTextField="USERNAME" DataValueField="USERID" OnSelectedIndexChanged="ddlLevel2_OnSelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            <br />
                                            <asp:DropDownList ID="ddlLevel3" AppendDataBoundItems="true" runat="server" CssClass="txtInput"
                                                AutoPostBack="true" Width="200px" Enabled="false" DataSourceID="ObjectDataSource_Users"
                                                DataTextField="USERNAME" DataValueField="USERID">
                                                <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="ObjectDataSource_Users" runat="server" SelectMethod="Get_UserList"
                                                TypeName="SMS.Business.Infrastructure.BLL_Infra_UserCredentials">
                                                <SelectParameters>
                                                    <asp:SessionParameter Name="CompanyID" SessionField="USERCOMPANYID" Type="Int32" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                        <asp:Panel ID="pnlBrowseFile" runat="server" Visible="true">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <tr>
                                        <td style="vertical-align: top; width: 130px;">
                                            <asp:Label ID="lblFormType" runat="server" Text="Form Type" Height="5px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlFormType" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">
                                            <asp:Label ID="lblDept" runat="server" Text="Department" Height="5px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDepartments" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">
                                            <asp:Label ID="lblFormat" runat="server" Text="Format" Height="5px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFormat" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">
                                            Browse File
                                        </td>
                                        <td style="vertical-align: top; text-align: left;">
                                            <asp:FileUpload ID="FileUploader" runat="server" CssClass="fileBrowser" Width="325px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; vertical-align: top;">
                                            <asp:Label ID="lblRAForms" runat="server" Text="Risk Assessment Forms"></asp:Label>
                                        </td>
                                        <td style="text-align: left; vertical-align: top;">
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DataList ID="dlRAForms" runat="server" RepeatColumns="5" RepeatDirection="Vertical"
                                                        RepeatLayout="Table" CellSpacing="2">
                                                        <ItemTemplate>
                                                            <div style="background-color: #C3EBFF; border-radius: 2px; padding: 1px; border: 1px solid #ACC9C9">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:HyperLink ID="hlRAForms" runat="server" Text='<%# Eval("Work_Category_Name")%>'
                                                                                Target="_blank" Font-Names="Calibri" NavigateUrl='<%# "~/JRA/Libraries/HazardTemplate.aspx?DocID="+Eval("Work_Categ_ID").ToString() %>'>
                                                                            </asp:HyperLink>
                                                                            <asp:HiddenField ID="hdnRAFrm" runat="server" Value='<%# Eval("Work_Categ_ID")%>' />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                    <asp:Button ID="lbtnRAForms" OnClick="OnClick_lbtnRAForms" runat="server" Text="Attach RA Forms"
                                                        Visible="true" ForeColor="Black"></asp:Button>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; vertical-align: top;">
                                            <asp:Label ID="lblRemarks" runat="server" Text="Remarks"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="server" Width="324px" Height="89px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                        <tr>
                            <td style="text-align: center" colspan="3">
                                <asp:Button ID="btnSave" runat="server" Text="Add Document" OnClick="btnSave_Click"
                                    OnClientClick="return specialcharecter();" />
                                <asp:Button ID="btnAddFolderAccess" runat="server" Text="Add Folder Access" Visible="false"
                                    OnClick="btnAddFolderAccess_Click" />
                                <asp:Button ID="btnRemoveFolderAccess" runat="server" Text="Remove Folder Access"
                                    Visible="false" OnClick="btnRemoveFolderAccess_Click" />
                                <asp:HiddenField ID="hdnVesionChecked" runat="server" />
                                <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
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
                                                <asp:Label ID="Label2" runat="server" Text="Edit Folder" Font-Bold="True" ForeColor="#003366"
                                                    ToolTip="Edit Folder"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width:130px;">
                                                <asp:Label ID="lblParentFolderE" runat="server" Text="Parent Folder"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtParentFolderE" ReadOnly="true" runat="server" Width="320px"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;">
                                                <asp:Label ID="lblFolderNameE" runat="server" Text="Folder Name"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFolderNameE" runat="server" Width="320px"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;">
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
                                                <%-- Click + Drag Down to select multiple Department<br />
                                        <br />
                                        OR<br />
                                        Hold Ctrl Key + Click on Department Name to select/un-select multiple Department--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;">
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
                                                <%--Click + Drag Down to select multiple Department<br />
                                        <br />
                                        OR<br />
                                        Hold Ctrl Key + Click on Department Name to select/un-select multiple Department--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 100px;">
                                                <asp:Label ID="lblApprovalE" runat="server" Text="Approval Required" Height="5px"></asp:Label><br />
                                                <br />
                                                <br />
                                                <asp:Label ID="lblLevel1E" runat="server" Text="Level1:"></asp:Label><br />
                                                <br />
                                                <asp:Label ID="lblLevel2E" runat="server" Text="Level2:"></asp:Label><br />
                                                <br />
                                                <asp:Label ID="lblLevel3E" runat="server" Text="Level3:"></asp:Label><br />
                                            </td>
                                            <td style="vertical-align: top;">
                                                <asp:CheckBox ID="chkApprovalE" runat="server" CssClass="bckColor" AutoPostBack="true"
                                                    OnCheckedChanged="chkApprovalE_OnCheckedChanged" /><br />
                                                <br />
                                                <asp:DropDownList ID="ddlUser1E" AppendDataBoundItems="true" runat="server" CssClass="txtInput"
                                                    AutoPostBack="true" Width="200px" Enabled="false" OnSelectedIndexChanged="ddlUser1E_OnSelectedIndexChanged">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:DropDownList ID="ddlUser2E" AppendDataBoundItems="true" runat="server" CssClass="txtInput"
                                                    AutoPostBack="true" Width="200px" Enabled="false" OnSelectedIndexChanged="ddlUser2E_OnSelectedIndexChanged">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:DropDownList ID="ddlUser3E" AppendDataBoundItems="true" runat="server" CssClass="txtInput"
                                                    AutoPostBack="true" Width="200px" Enabled="false">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="text-align: center">
                                                <asp:Button ID="btnSaveE" runat="server" Text="Save" OnClick="btnSaveE_Click" />
                                                <asp:Button ID="btnAddFolderAccessE" runat="server" Text="Add Folder Access" OnClick="btnAddFolderAccessE_Click"
                                                    Visible="false" />
                                                <asp:Button ID="btnRemoveFolderAccessE" runat="server" Text="Remove Folder Access"
                                                    OnClick="btnRemoveFolderAccessE_Click" Visible="false" />
                                                <asp:Button ID="btnDeleteE" runat="server" Text="Delete Folder" OnClick="btnDeleteE_Click"
                                                    Visible="false" OnClientClick="return confirm('Are you sure want to delete?')" />
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
        <div id="fragment3" runat="server" style="padding: 0px; display: inline">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="border: 1px solid #BDBDBD; padding: 2px;">
                        <div style="overflow: hidden; padding: 2px; margin-bottom: 1px; border: 1px solid outset;">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width:40%;">
                                        Search in Folder:
                                        <asp:Label ID="lblParentFolder" runat="server"></asp:Label>
                                    </td>
                                    <td style="text-align: right;width:60%;">
                                        <table style="background-image: url(images/searchbox.png); background-repeat: no-repeat;
                                            background-position: 0 1px; padding: 1px;" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="padding-left: 2px; " >
                                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="noborder" OnTextChanged="txtSearch_TextChanged"
                                                        AutoPostBack="true"></asp:TextBox><img src="images/search.gif" />
                                                </td>
                                                <td style="width: 5px">
                                                </td>
                                                <td style="padding-right: 2px;">
                                                    <asp:ImageButton ID="ImgBtnAdvSearch" ImageUrl="images/search.png" runat="server"
                                                        OnClick="ImgBtnAdvSearch_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="pnlAdvSearch" runat="server" Visible="false">
                                <table width="100%" cellpadding="2" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            Modification date:
                                        </td>
                                        <td>
                                            Between:
                                            <asp:TextBox ID="txtFromDate" runat="server" Text="" MaxLength="40" Width="90px" onkeypress="return searchKeyPressDate(event);"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtFromDate"
                                                Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td>
                                            And:
                                            <asp:TextBox ID="txtToDate" runat="server" Text="" MaxLength="40" Width="90px" onkeypress="return searchKeyPressDate(event);"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtToDate"
                                                Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td align="right" colspan="2">
                                            <asp:Button ID="btnExecute" runat="server" Text="Retrieve" OnClick="btnRetrieve_Click" OnClientClick="return ValidationOnDate();"/>
                                            <asp:Button ID="Button1" runat="server" Text="Clear" OnClick="btnClear_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                        <asp:Panel ID="pnlViewSearchResult" runat="server">
                            <asp:GridView ID="gvSearchResult" runat="server" BackColor="White" BorderColor="White"
                                AutoGenerateColumns="False" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3"
                                CellSpacing="1" GridLines="None" OnRowDataBound="gvSearchResult_RowDataBound"
                                DataKeyNames="Filename" AllowSorting="True" PageSize="18" OnPageIndexChanging="gvSearchResult_PageIndexChanging"
                                Width="100%" OnSorting="gvSearchResult_Sorting">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="Label1" runat="server" Text="No Records Found" ForeColor="Red" Font-Bold="true"></asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="File Name" HeaderStyle-Font-Bold="true" SortExpression="Filename" HeaderStyle-HorizontalAlign="center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("Filename") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size(KB)" HeaderStyle-Font-Bold="true" SortExpression="File Size" HeaderStyle-HorizontalAlign="center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSize" runat="server" Text='<%# Eval("File Size") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Modified" HeaderStyle-Font-Bold="true" SortExpression="write" HeaderStyle-HorizontalAlign="center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblwrite" runat="server" Text='<%# Eval("Read Date") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Version" HeaderText="Version" SortExpression="Version" HeaderStyle-HorizontalAlign="center" />
                                    <asp:TemplateField HeaderText="View" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="center" 
                                        ItemStyle-VerticalAlign="Middle" HeaderStyle-Height="10px">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="ImgOpenForView" ImageUrl="~/FMS/images/ie.png"
                                                Height="15px" Width="15px" ImageAlign="Middle" ToolTip="view in Browser" />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Height="10px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpostdtNotVissible" runat="server" Visible="false" Text='<%# Eval("Write")%>'></asp:Label>
                                            <asp:ImageButton runat="server" ID="ImgOpen" ImageUrl="~/FMS/images/ie.png" Height="15px"
                                                Width="15px" CommandName='<%# Eval("FilePath") %>' OnCommand="ShowFile" ImageAlign="Middle"
                                                ToolTip="view in Browser" />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Height="10px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View in external File" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="center"
                                        ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Height="10px" >
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="ImgOpenExt" ImageUrl="images/windowopen.ico"
                                                Height="15px" Width="15px" CommandName='<%# Eval("FilePath") %>' ImageAlign="Middle"
                                                OnCommand="OpenFileExternal" ToolTip="Open in External Application" />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Height="10px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                    <div id="divMessage" align="center">
                        <asp:Label ID="dvMessage" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="fragment4" runat="server" style="padding: 0px; display: inline">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvScheduleHistory" runat="server" EmptyDataText="NO RECORDS FOUND"
                        AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="1" CellSpacing="0"
                        Width="100%" GridLines="both" AllowSorting="true" CssClass="gridmain-css">
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                        <PagerStyle CssClass="PMSPagerStyle-css" />
                        <SelectedRowStyle BackColor="#FFFFCC" />
                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                        <Columns>
                            <asp:TemplateField HeaderText="File Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblFileName" runat="server" Text='<%#Eval("File_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vessel">
                                <ItemTemplate>
                                    <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Schedule_Desc")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblSchDate" runat="server" Text='<%#Eval("Schedule_Date")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Completion Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblLastDone" runat="server" Text='<%#Eval("Completion_Date")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Next Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDue" runat="server" Text='<%# Eval("Next_Due_Date") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="Reminder">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblReminder" runat="server" Text='<%#Eval("Reminder")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                                    </ItemStyle>
                                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                                </asp:TemplateField>--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <a href="FMSFileLoader.aspx?DocID=<%# Eval("FileID")%>">View</a>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Verified">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImgUpdate" runat="server" Visible='<%# Eval("[ID]")%>' ToolTip="Verified"
                                                                    ImageUrl="~/Images/DocTree/checked.gif" Height="16px"></asp:ImageButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                        </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>
                    <uc1:ucCustomPager ID="ucCustomPagerSch" runat="server" PageSize="30" OnBindDataItem="GetScheduleHistory" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="divRAFC" runat="server">
        <asp:UpdatePanel ID="udpRACategory" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="divRACategory" title="Attach RA Forms" style="display: none; border: 1px solid Gray;
                    font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 250px;
                    height: 400px;">
                    <table>
                        <tr>
                            <td style="width: 5px">
                                <asp:Label ID="Label3" runat="server" Text="Search" Font-Size="12px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCatSearch" runat="server" Width="160px">
                                </asp:TextBox>
                            </td>
                            <td style="float: left;">
                                <asp:ImageButton ID="imgbtnSearch" ImageUrl="~/images/search.gif" runat="server"
                                    OnClick="imgbtnSearch_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="btnAdd" runat="server" Text="Save" OnClick="btnAdd_Click"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lblRAFrm" runat="server" Text="RA Forms" Font-Bold="true" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div style="overflow-y: scroll; height: 300px; border: 1px solid black;">
                                    <asp:CheckBoxList ID="chklRAF_Category" runat="server" Width="230px">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
