<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNewFile.aspx.cs" Inherits="AddNewFile" %>

<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Add File Form</title>
    <script type="text/javascript" src="../Scripts/jquery-1.4.4.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui.min.js"></script>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
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
    <div style="background-color: Yellow; font-size: 12px; color: Red; text-align: center;">
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </div>
    <div id="tabs" style="margin-top: 0px; width: 99%; display: block;">
        <ul>
            <li><a href="#fragment-0"><span>New Folders</span></a></li>
            <li><a href="#fragment-1"><span>Vessel Mapping</span></a></li>
            <li><a href="#fragment-2"><span>User Mapping</span></a></li>
            <li><a href="#fragment-3"><span>New Procedure</span></a></li>
        </ul>
        <div id="fragment-0" style="padding: 0px; display: block">
            <div style="border: 1px solid #BDBDBD; background-color: #F5EFFB; padding: 10px;
                text-align: left;">
                <asp:Panel ID="pnlUploadDocument" runat="server">
                    <table cellpadding="2" cellspacing="0" style="border: 1px solid #dcdcdc; width: 100%;">
                        <tr>
                            <td align="center" valign="top" colspan="3">
                                <asp:Label ID="lblFormTitle" runat="server" Text="New Folder" Font-Bold="True" ForeColor="#003366"
                                    ToolTip="New Folder"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                <asp:Label ID="lblDocName" runat="server" Text="Parent Folder"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFolderList" runat="server" Width="320px" Height="25px">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                <asp:Label ID="Label3" runat="server" Text="Folder Name" Enabled="true"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFolderName" runat="server" Width="324px" MaxLength="50" Height="20px"
                                    Enabled="true"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center" colspan="3">
                                <asp:Button ID="btnAddNew" runat="server" Text="Create Folder" OnClick="btnSave_Click"
                                    BorderStyle="Solid" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                    Height="24px" BackColor="#81DAF5" />
                                <asp:Button ID="btnRenameFolder" runat="server" Text="Re-Name" OnClick="btnRenameFolder_Click"
                                    BorderStyle="Solid" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                    Height="24px" BackColor="#81DAF5" />
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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table cellpadding="2" cellspacing="0" style="border: 1px solid #dcdcdc; width: 100%;">
                            <tr style="width: 30px">
                                <td align="center" valign="top" colspan="3">
                                    <asp:Label ID="Label2" runat="server" Text="Vessel Mapping" Font-Bold="True" ForeColor="#003366"
                                        ToolTip="Vessel Mapping"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    Fleet :
                                </td>
                                <td valign="top" align="left" style="height: 30px; width: 170px">
                                    <ucDDL:ucCustomDropDownList ID="DDLFleet" runat="server" UseInHeader="false" OnApplySearch="DDLFleet_SelectedIndexChanged"
                                        Height="150" Width="160" />
                                </td>
                                <td align="right" valign="top">
                                    Vessel :
                                </td>
                                <td valign="top" align="left" style="height: 30px">
                                    <ucDDL:ucCustomDropDownList ID="DDLVessel" runat="server" UseInHeader="false" OnApplySearch="DDLVessel_SelectedIndexChanged"
                                        Height="200" Width="160" />
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; border: 1px solid #cccccc; width: 25%;">
                                    <div style="height: 400px; overflow: auto;">
                                        <asp:TreeView ID="trvFolder" runat="server" ImageSet="XPFileExplorer" NodeIndent="15"
                                            Width="100%" BorderColor="#cccccc" OnSelectedNodeChanged="trvFolder_SelectedNodeChanged">
                                            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                            <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="2px"
                                                NodeSpacing="0px" VerticalPadding="2px" />
                                            <ParentNodeStyle Font-Bold="False" />
                                            <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                                                VerticalPadding="0px" CssClass="SelectedNodeStyle" />
                                        </asp:TreeView>
                                    </div>
                                </td>
                                <td style="vertical-align: top; border: 1px solid #cccccc;" colspan="3">
                                    <asp:GridView ID="gvFolderAccessVessel" runat="server" AutoGenerateColumns="False"
                                        AllowPaging="True" PageSize="20" DataKeyNames="Folder_ID" Width="100%" CellPadding="4"
                                        ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Folder Name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFolderId" runat="server" Text='<%#Eval("Folder_ID") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblFolderName" runat="server" Text='<%#Eval("Folder_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vessel Name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVesselId" runat="server" Text='<%#Eval("Vessel_ID") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblFleet" runat="server" Text='<%#Eval("FLEETCODE") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("Vessel_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Assign Vessel">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkVessel" runat="server" ForeColor="white" Checked='<%# Convert.ToBoolean(Eval("VESSEL_ACCESS")) %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblNoRec" runat="server" Text="Click on Initialize Folder Access to Vessel"></asp:Label>
                                        </EmptyDataTemplate>
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle CssClass="pager" HorizontalAlign="Left" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                    <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                                        background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <uc1:ucCustomPager ID="ucCustomPager1" runat="server" PageSize="15" OnBindDataItem="BindFolderToVEsssel" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="border: 1px solid outset; text-align: right;">
                                        <%--<asp:Button ID="btnInitialize" runat="server" Text="Initialize Access" OnClick="btnInitializeMenu_Click" />--%>
                                        <asp:Button ID="btnVesselRemove" runat="server" Text="Remove Access" OnClick="btnVesselRemove_Click"
                                            BorderStyle="Solid" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                            Height="24px" BackColor="#81DAF5" />
                                        <asp:Button ID="btnVesselSave" runat="server" Text="Save Changes" OnClick="btnVesselSave_Click"
                                            BorderStyle="Solid" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                            Height="24px" BackColor="#81DAF5" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center" colspan="4">
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="fragment-2" style="padding: 0px; display: block">
            <div style="border: 1px solid #BDBDBD; background-color: #F5EFFB; padding: 10px;
                text-align: left;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table cellpadding="3" cellspacing="0" style="border: 1px solid #dcdcdc; width: 100%;">
                            <tr style="width: 30px">
                                <td align="center" valign="top" colspan="4">
                                    <asp:Label ID="Label4" runat="server" Text="User Mapping" Font-Bold="True" ForeColor="#003366"
                                        ToolTip="User Mapping"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top;" align="right">
                                    Visible to Department
                                </td>
                                <td style="vertical-align: top">
                                    <asp:DropDownList ID="lstDept" runat="server" Width="230px" AutoPostBack="true" OnSelectedIndexChanged="lstDept_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td style="vertical-align: top;" width="120px">
                                    Visible to Users
                                </td>
                                <td style="vertical-align: top">
                                    <asp:DropDownList ID="lstUser" runat="server" Width="230px" DataTextField="UserName"
                                        DataValueField="UserID" AutoPostBack="true" OnSelectedIndexChanged="lstUser_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; border: 1px solid #cccccc; width: 25%;">
                                    <div style="height: 400px; overflow: auto;">
                                        <asp:TreeView ID="TreeView1" runat="server" ImageSet="XPFileExplorer" NodeIndent="15"
                                            Width="100%" BorderColor="#cccccc" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                                            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                            <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="2px"
                                                NodeSpacing="0px" VerticalPadding="2px" />
                                            <ParentNodeStyle Font-Bold="False" />
                                            <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                                                VerticalPadding="0px" CssClass="SelectedNodeStyle" />
                                        </asp:TreeView>
                                    </div>
                                </td>
                                <td style="vertical-align: top; border: 1px solid #cccccc;" colspan="3">
                                    <asp:GridView ID="dvFolderUserAccess" runat="server" AutoGenerateColumns="False"
                                        AllowPaging="True" PageSize="20" DataKeyNames="FOLDER_ID" Width="100%" CellPadding="4"
                                        ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Menu">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFolderName" runat="server" Text='<%#Eval("FOLDER_NAME") %>'></asp:Label>
                                                    <asp:Label ID="lblFolderId" runat="server" Visible="false" Text='<%#Eval("FOLDER_ID") %>'></asp:Label>
                                                    <asp:Label ID="lblDeptId" runat="server" Visible="false" Text='<%#Eval("Dep_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="USER">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUerId" runat="server" Visible="false" Text='<%#Eval("UserID") %>'></asp:Label>
                                                    <asp:Label ID="lblLink" runat="server" Text='<%#Eval("First_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Select All">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" ForeColor="white" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkView" runat="server" Checked='<%# Convert.ToBoolean(Eval("access_view")) %>'
                                                        ForeColor="white" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Add">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkAdd" runat="server" Checked='<%# Convert.ToBoolean(Eval("access_add")) %>'
                                                        ForeColor="white" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkEdit" runat="server" Checked='<%# Convert.ToBoolean(Eval("access_edit")) %>'
                                                        ForeColor="white" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkDelete" runat="server" Checked='<%# Convert.ToBoolean(Eval("access_delete")) %>'
                                                        ForeColor="white" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblNoRec" runat="server" Text="Click on Initialize Folder Access to user"></asp:Label>
                                        </EmptyDataTemplate>
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle CssClass="pager" HorizontalAlign="Left" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                    <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                                        background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="15" OnBindDataItem="BindFolderToUserAccess" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="border: 1px solid outset; text-align: right;">
                                        <%--<asp:Button ID="btnInitialize" runat="server" Text="Initialize Access" OnClick="btnInitializeMenu_Click" />--%>
                                        <asp:Button ID="btnResetMenu" runat="server" Text="Remove Access" OnClick="btnResetMenu_Click"
                                            BorderStyle="Solid" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                            Height="24px" BackColor="#81DAF5" />
                                        <asp:Button ID="btnAccessSave" runat="server" Text="Save Changes" OnClick="btnAccessSave_Click"
                                            BorderStyle="Solid" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                            Height="24px" BackColor="#81DAF5" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="fragment-3" style="padding: 0px; display: block">
            <div style="border: 1px solid #BDBDBD; background-color: #F5EFFB; padding: 10px;
                text-align: left;">
                <asp:UpdatePanel runat="server" ID="updatepnl">
                    <ContentTemplate>
                        <asp:Panel ID="Panel1" runat="server">
                            <table cellpadding="2" cellspacing="0" style="border: 1px solid #dcdcdc; width: 100%;">
                                <tr>
                                    <td style="vertical-align: top;">
                                        Procedure Code
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtProcedureCode" runat="server" Width="120px" MaxLength="10" Enabled="true"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rqvtxtProcedureCode" ValidationGroup="vgCreateProcedure"
                                            runat="server" ControlToValidate="txtProcedureCode" Display="None" ErrorMessage="Please enter procedure code"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vcaltxtProcedureCode" runat="server" TargetControlID="rqvtxtProcedureCode">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">
                                        Procedure Name
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtProcedureName" runat="server" Width="240px" MaxLength="50" Enabled="true"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rqvtxtProcedureName" ValidationGroup="vgCreateProcedure"
                                            runat="server" Display="None" ControlToValidate="txtProcedureName" ErrorMessage="Please enter procedure name"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vcalddlFolderName" runat="server" TargetControlID="rqvtxtProcedureName">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">
                                        Folder Name
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlFolderName" runat="server" Width="220px" Height="25px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">
                                        Required Watermark ?
                                    </td>
                                    <td align="left">
                                        <asp:CheckBox ID="FileWateMark" runat="server" Checked="false" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">
                                        Header Template
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlHeaderTemplate" runat="server" Width="220px" Height="25px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">
                                        Footer Template
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlFooterTemlate" runat="server" Width="220px" Height="25px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="center">
                                        <asp:Button ID="btnSaveCommand" runat="server" Text="Create Procedure" OnClick="btnSaveCommand_Click"
                                            ValidationGroup="vgCreateProcedure" BorderStyle="Solid" BorderColor="#0489B1"
                                            BorderWidth="1px" Font-Names="Tahoma" Height="24px" BackColor="#81DAF5" />
                                        <asp:Button ID="btnCancelCommand" runat="server" Text="Cancel" BorderStyle="Solid"
                                            OnClick="btnCancelCommand_Click" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                            Height="24px" BackColor="#81DAF5" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="divMessage" align="center">
        <asp:Label ID="dvMessage" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label>
    </div>
    </form>
</body>
</html>
