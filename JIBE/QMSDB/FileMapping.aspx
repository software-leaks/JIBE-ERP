<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FileMapping.aspx.cs" Inherits="QMSDB_FileMapping" %>
<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
 <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>

    <div id="tabs" style="margin-top: 0px; width: 99%; display: block;">
        <ul>
            <li><a href="#fragment-0"><span>New Procedure</span></a></li>
            <li><a href="#fragment-1"><span>Vessel Mapping</span></a></li>
            <li><a href="#fragment-2"><span>User Mapping</span></a></li>
        </ul>
    
        <div id="fragment-0" style="padding: 0px; display: block">
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
                                        Is Watermark
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
        <div id="fragment-1" style="padding: 0px; display: block">
            <div style="border: 1px solid #BDBDBD; background-color: #F5EFFB; padding: 10px;
                text-align: left;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table cellpadding="2" cellspacing="0" style="border: 1px solid #dcdcdc; width: 100%;">
                            <tr style="width: 30px">
                                <td align="center" valign="top" colspan="4">
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
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="vertical-align: top; border: 1px solid #cccccc;">
                                    <div style="height: 400px; overflow: auto;">
                                        <asp:ListBox ID="DDLVessel" runat="server" Width="200" Height="380" AutoPostBack="true" OnSelectedIndexChanged="DDLVessel_SelectedIndexChanged" >
                                        </asp:ListBox>
                                        <%-- <ucDDL:ucCustomDropDownList ID="DDLVessel" runat="server" UseInHeader="false" OnApplySearch="DDLVessel_SelectedIndexChanged"
                                        Height="200" Width="160" />--%>
                                    </div>
                                </td>
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
                                    <asp:GridView ID="gvVesselAccess" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                        PageSize="20" DataKeyNames="PROCEDURE_ID" Width="100%" CellPadding="4" ForeColor="#333333"
                                        GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Folder Name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProcedureId" runat="server" Text='<%#Eval("PROCEDURE_ID") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblFolderName" runat="server" Text='<%#Eval("Folder_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Procedure Code">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProcedureCode" runat="server" Text='<%#Eval("PROCEDURE_CODE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Procedure Name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProcedureName" runat="server" Text='<%#Eval("PROCEDURES_NAME") %>'></asp:Label>
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
                                            <asp:Label ID="lblNoRec" runat="server" Text="Click on Initialize procedure Access to  vessel "></asp:Label>
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
                                    <asp:HiddenField ID="hdnVesionChecked" runat="server" />
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
                                <td style="vertical-align: top;" >
                                   Department
                                </td>
                                <td style="vertical-align: top">
                                    <asp:DropDownList ID="lstDept" runat="server" Width="120px" AutoPostBack="true" SelectionMode="Multiple"
                                        OnSelectedIndexChanged="lstDept_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2"  style="vertical-align: top; border: 1px solid #cccccc; width: 25%;">
                                    <div style="height: 400px; overflow: auto;">
                                        <asp:ListBox ID="lstUser" runat="server" Width="230px" Height ="400px"  AutoPostBack="true"
                                            SelectionMode="Multiple" DataTextField="UserName"
                                            DataValueField="UserID" 
                                            onselectedindexchanged="lstUser_SelectedIndexChanged"></asp:ListBox>
                                    </div>
                                </td>
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
                                    <asp:GridView ID="gvUserProcedure" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                        PageSize="20" DataKeyNames="PROCEDURE_ID" Width="100%" CellPadding="4" ForeColor="#333333"
                                        GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Folder Name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProcedureId" runat="server" Text='<%#Eval("PROCEDURE_ID") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblFolderName" runat="server" Text='<%#Eval("Folder_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Procedure Code">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProcedureCode" runat="server" Text='<%#Eval("PROCEDURE_CODE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Procedure Name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProcedureName" runat="server" Text='<%#Eval("PROCEDURES_NAME") %>'></asp:Label>
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
                                            <asp:Label ID="lblNoRec" runat="server" Text="Click on Initialize procedure Access to user"></asp:Label>                                  
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
    </div>
    <div id="divMessage" align="center">
        <asp:Label ID="dvMessage" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label>
    </div>
</asp:Content>
