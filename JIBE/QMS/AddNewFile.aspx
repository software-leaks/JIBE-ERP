<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNewFile.aspx.cs" Inherits="AddNewFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Add File Form</title>
    <script type="text/javascript" src="../Scripts/jquery-1.4.4.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui.min.js"></script>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="JS/common.js" type="text/javascript"></script>
    <link href="css/main.css" rel="stylesheet" type="text/css" />
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
    <div style="background-color: Yellow; font-size: 12px; color: Red; text-align: center;">
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </div>
    <div id="tabs" style="margin-top: 0px; width: 99%; display: block;">
        <ul>
            <li><a href="#fragment-0"><span>Add New File/Folder</span></a></li>
            <li><a href="#fragment-1"><span>Edit Folder</span></a></li>
            <li><a href="#fragment-2"><span>Search Document</span></a></li>
        </ul>
        <div id="fragment-0" style="padding: 0px; display: inline">
            <div style="border: 1px solid #BDBDBD; background-color: #F5EFFB; padding: 10px;
                text-align: left;">
                <asp:Panel ID="pnlUploadDocument" runat="server">
                    <table cellpadding="2" cellspacing="0" style="border: 1px solid #dcdcdc; width: 100%;">
                        <tr>
                            <td align="center" valign="top" colspan="3">
                                <asp:Label ID="lblFormTitle" runat="server" Text="Add New File/Folder" Font-Bold="True"
                                    ForeColor="#003366" ToolTip="Add New File/Folder"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
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
                            <asp:UpdatePanel ID="update1" runat="server" UpdateMode="Conditional" >
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
                                            Visible to Department
                                        </td>
                                        <td style="vertical-align: top">
                                           <%-- <asp:ListBox ID="lstDept" runat="server" Width="330px" Height="185px" AutoPostBack="true"
                                                    SelectionMode="Multiple" OnSelectedIndexChanged="lstDept_SelectedIndexChanged">
                                                </asp:ListBox>--%>
                                            <div style="max-height: 180px; height: 180px; width: 330px; overflow-y: auto; background-color: White;
                                                border-color: Gray; border-style: solid; border-width: 1px">
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
                                            Visible to Users
                                        </td>
                                        <td style="vertical-align: top">
                                              <%-- <asp:ListBox ID="lstUser" runat="server" Width="330px" Height="180px" SelectionMode="Multiple"
                                                    DataTextField="UserName" DataValueField="UserID"></asp:ListBox>--%>
                                            <div style="max-height: 180px; height: 180px; width: 330px; overflow-y: auto; background-color: White;
                                                border-color: Gray; border-style: solid; border-width: 1px">
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
                                    <asp:Label ID="lblRemarks" runat="server" Text="Remarks"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="server" Width="324px" Height="89px"></asp:TextBox>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td style="text-align: center" colspan="3">
                                <asp:Button ID="btnSave" runat="server" Text="Add Document" OnClick="btnSave_Click" />
                                <asp:Button ID="btnAddFolderAccess" runat="server" Text="Add Folder Access" Visible="false"
                                    OnClick="btnAddFolderAccess_Click" />
                                <asp:Button ID="btnRemoveFolderAccess" runat="server" Text="Remove Folder Access"
                                    Visible="false" OnClick="btnRemoveFolderAccess_Click" />
                                <asp:HiddenField ID="hdnVesionChecked" runat="server" />
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>
        <div id="fragment-1" style="padding: 0px; display: block">
            <div style="border: 1px solid #BDBDBD; background-color: #F5EFFB; padding: 10px;
                text-align: left;">
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
                                    <td style="vertical-align: top;">
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
                                        Visible to Department
                                    </td>
                                    <td style="vertical-align: top">
                                        <%--<asp:ListBox ID="lstDept" runat="server" Width="330px" Height="185px" AutoPostBack="true"
                                                    SelectionMode="Multiple" OnSelectedIndexChanged="lstDept_SelectedIndexChanged">
                                                </asp:ListBox>--%>
                                        <div style="max-height: 180px; height: 180px; width: 330px; overflow-y: auto; background-color: White;
                                            border-color: Gray; border-style: solid; border-width: 1px">
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
                                        Visible to Users
                                    </td>
                                    <td style="vertical-align: top">
                                        <div style="max-height: 180px; overflow-y: auto; background-color: White; border-color: Gray;
                                            border-style: solid; border-width: 1px; max-width: 330px">
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
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: center">
                                        <asp:Button ID="btnSaveE" runat="server" Text="Edit Folder" OnClick="btnSaveE_Click" />
                                        <asp:Button ID="btnAddFolderAccessE" runat="server" Text="Add Folder Access" OnClick="btnAddFolderAccessE_Click" />
                                        <asp:Button ID="btnRemoveFolderAccessE" runat="server" Text="Remove Folder Access"
                                            OnClick="btnRemoveFolderAccessE_Click" />
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
        </div>
        <div id="fragment-2" style="padding: 0px; display: block">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="border: 1px solid #BDBDBD; background-color: #F5EFFB; padding: 2px;">
                        <div style="overflow: hidden; padding: 2px; background-color: #cfcfff; margin-bottom: 1px;
                            border: 1px solid outset;">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        Search in Folder:
                                        <asp:Label ID="lblParentFolder" runat="server"></asp:Label>
                                    </td>
                                    <td style="text-align: right">
                                        <table style="background-image: url(images/searchbox.png); background-repeat: no-repeat;
                                            background-position: 0 1px; padding: 1px;" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="padding-left: 2px">
                                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="noborder" OnTextChanged="txtSearch_TextChanged"
                                                        AutoPostBack="true"></asp:TextBox><img src="images/search.gif" />
                                                </td>
                                                <td style="width: 5px">
                                                </td>
                                                <td style="padding-right: 2px">
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
                                            Modified Between:
                                        </td>
                                        <td>
                                            From Date:
                                            <asp:TextBox ID="txtFromDate" runat="server" Text="" MaxLength="40" Width="90px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtFromDate"
                                                Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td>
                                            To Date:
                                            <asp:TextBox ID="txtToDate" runat="server" Text="" MaxLength="40" Width="90px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtToDate"
                                                Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td align="right" colspan="2">
                                            <asp:Button ID="btnExecute" runat="server" Text="Retrieve" OnClick="btnRetrieve_Click" />
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
                                <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="Label1" runat="server" Text="No document found for the search !!"></asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="File Name" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true"
                                        SortExpression="Filename">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("Filename") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" ForeColor="White"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size(KB)" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true"
                                        SortExpression="File Size">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSize" runat="server" Text='<%# Eval("File Size") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" ForeColor="White"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Modified" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true"
                                        SortExpression="write">
                                        <ItemTemplate>
                                            <asp:Label ID="lblwrite" runat="server" Text='<%# Eval("Read Date") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" ForeColor="White"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Version" HeaderText="Version" SortExpression="Version" />
                                    <asp:TemplateField HeaderText="View" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true"
                                        ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Height="10px">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="ImgOpenForView" ImageUrl="~/QMS/images/ie.png"
                                                Height="15px" Width="15px" ImageAlign="Middle" ToolTip="view in Browser" />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" ForeColor="White" Height="10px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpostdtNotVissible" runat="server" Visible="false" Text='<%# Eval("write")%>'></asp:Label>
                                            <asp:ImageButton runat="server" ID="ImgOpen" ImageUrl="~/QMS/images/ie.png" Height="15px"
                                                Width="15px" CommandName='<%# Eval("FilePath") %>' OnCommand="ShowFile" ImageAlign="Middle"
                                                ToolTip="view in Browser" />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" ForeColor="White" Height="10px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View in external File" HeaderStyle-ForeColor="White"
                                        HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                        HeaderStyle-Height="10px">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="ImgOpenExt" ImageUrl="images/windowopen.ico"
                                                Height="15px" Width="15px" CommandName='<%# Eval("FilePath") %>' ImageAlign="Middle"
                                                OnCommand="OpenFileExternal" ToolTip="Open in External Application" />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" ForeColor="White" Height="10px"></HeaderStyle>
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
    </div>
    </form>
</body>
</html>
