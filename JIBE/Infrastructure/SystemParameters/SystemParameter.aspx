<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SystemParameter.aspx.cs"
    Inherits="Sys_parameters_SystemParameter" Title="System Parameter Non module(Search/edit)" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="contenthead" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ListSearchExtenderPrompt
        {
            font-style: italic;
            color: Gray;
            background-color: White;
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
<asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
    <asp:Panel runat="server" ID="mainPanel">
        <%--
<div id="page-header">
System Parameter
</div>--%>
        <center>
            <div id="main" style="height: 420; width: 100%;" onkeyup="return listboxTest(this)">
                <asp:UpdatePanel ID="tblPanel" runat="server">
                    <ContentTemplate>
                        <div style="float: left">
                            <b>Infrastructure > System Parameter > Main</b>
                        </div>
                        <br />
                        <table width="100%" cellpadding="0" cellspacing="1">
                            <thead class="HeaderStyle-css">
                                <%--  <tr>
                                    <td align="left" colspan="7">
                                       
                                    </td>
                                </tr>--%>
                                <tr style="height: 30px; vertical-align: middle;">
                                    <td style="border: 1px; border-left: white">
                                        <b>Module </b>
                                        <asp:DropDownList ID="ddlModule" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged">
                                            <asp:ListItem Value="1">Show Active</asp:ListItem>
                                            <asp:ListItem Value="0">Show Deleted</asp:ListItem>
                                            <asp:ListItem Value="2">Show All</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <b>Level-1 </b>
                                        <asp:DropDownList ID="ddlActive1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlActive1_SelectedIndexChanged">
                                            <asp:ListItem Value="1">Show Active</asp:ListItem>
                                            <asp:ListItem Value="0">Show Deleted</asp:ListItem>
                                            <asp:ListItem Value="2">Show All</asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                    </td>
                                    <td>
                                        <b>Level-2</b>
                                        <asp:DropDownList ID="ddlActive2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlActive2_SelectedIndexChanged">
                                            <asp:ListItem Value="1">Show Active</asp:ListItem>
                                            <asp:ListItem Value="0">Show Deleted</asp:ListItem>
                                            <asp:ListItem Value="2">Show All</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <b>Level-3</b>
                                        <asp:DropDownList ID="ddlActive3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlActive3_SelectedIndexChanged">
                                            <asp:ListItem Value="1">Show Active</asp:ListItem>
                                            <asp:ListItem Value="0">Show Deleted</asp:ListItem>
                                            <asp:ListItem Value="2">Show All</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <b>Level-4</b>
                                        <asp:DropDownList ID="ddlActive4" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlActive4_SelectedIndexChanged">
                                            <asp:ListItem Value="1">Show Active</asp:ListItem>
                                            <asp:ListItem Value="0">Show Deleted</asp:ListItem>
                                            <asp:ListItem Value="2">Show All</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <b>Level-5</b>
                                        <asp:DropDownList ID="ddlActive5" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlActive5_SelectedIndexChanged">
                                            <asp:ListItem Value="1">Show Active</asp:ListItem>
                                            <asp:ListItem Value="0">Show Deleted</asp:ListItem>
                                            <asp:ListItem Value="2">Show All</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <b>Level-6</b>
                                        <asp:DropDownList ID="ddlActive6" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlActive6_SelectedIndexChanged">
                                            <asp:ListItem Value="1">Show Active</asp:ListItem>
                                            <asp:ListItem Value="0">Show Deleted</asp:ListItem>
                                            <asp:ListItem Value="2">Show All</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr valign="middle">
                                    <td>
                                        <table cellpadding="0" cellspacing="0" width="99%">
                                            <tr>
                                                <td align="right">
                                                    <asp:TextBox ID="txtSearchModule" Width="130px" runat="server" 
                                                        style="background-color: #FFFFCC"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="imgModuleSearch" ImageUrl="~/Purchase/Image/preview.gif" Height="20px"
                                                        Width="20px" runat="server" OnClick="imgModuleSearch_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" width="99%">
                                            <tr>
                                                <td align="right">
                                                    <asp:TextBox ID="txtSearchLevel1" Width="130px" runat="server" 
                                                        style="background-color: #FFFFCC"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="imgLevel1Search" ImageUrl="~/Purchase/Image/preview.gif" Height="20px"
                                                        Width="20px" runat="server" OnClick="imgLevel1Search_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" width="99%">
                                            <tr>
                                                <td align="right">
                                                    <asp:TextBox ID="txtSearchLevel2" Width="130px" runat="server" 
                                                        style="background-color: #FFFFCC"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="imgLevel2Search" ImageUrl="~/Purchase/Image/preview.gif" Height="20px"
                                                        Width="20px" runat="server" OnClick="imgLevel2Search_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" width="99%">
                                            <tr>
                                                <td align="right">
                                                    <asp:TextBox ID="txtSearchLevel3" Width="130px" runat="server" 
                                                        style="background-color: #FFFFCC"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="imgLevel3Search" ImageUrl="~/Purchase/Image/preview.gif" Height="20px"
                                                        Width="20px" runat="server" OnClick="imgLevel3Search_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" width="99%">
                                            <tr>
                                                <td align="right">
                                                    <asp:TextBox ID="txtSearchLevel4" Width="130px" runat="server" 
                                                        style="background-color: #FFFFCC"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="imgLevel4Search" ImageUrl="~/Purchase/Image/preview.gif" Height="20px"
                                                        Width="20px" runat="server" OnClick="imgLevel4Search_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" width="99%">
                                            <tr>
                                                <td align="right">
                                                    <asp:TextBox ID="txtSearchLevel5" Width="130px" runat="server" 
                                                        style="background-color: #FFFFCC"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="imgLevel5Search" ImageUrl="~/Purchase/Image/preview.gif" Height="20px"
                                                        Width="20px" runat="server" OnClick="imgLevel5Search_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" width="99%">
                                            <tr>
                                                <td align="right">
                                                    <asp:TextBox ID="txtSearchLevel6" Width="130px" runat="server" 
                                                        style="background-color: #FFFFCC"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="imgLevel6Search" ImageUrl="~/Purchase/Image/preview.gif" Height="20px"
                                                        Width="20px" runat="server" OnClick="imgLevel6Search_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </thead>
                        </table>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <%--<tr align="center" class="RowStyle-css" >--%>
                            <tr align="center">
                                <td style="color: White; width: 120px">
                                    <%--   <cc1:ListSearchExtender ID="LstSearchExtModule" QueryPattern="Contains" PromptCssClass="ListSearchExtenderPrompt"
                                        TargetControlID="lsbModule" runat="server" 
                                          PromptPosition="Top" IsSorted="true" >
                                    </cc1:ListSearchExtender>--%>
                                    <asp:ListBox ID="lsbModule" runat="server" Height="410px" Width="173px" onKeyUp="listboxTest(this)"
                                        onDblClick="doubleClick(this)"></asp:ListBox>
                                </td>
                                <td>
                                    <%-- <cc1:ListSearchExtender ID="LstSearchExtLevel1" QueryPattern="Contains" TargetControlID="LsbLevel1" runat="server" PromptCssClass="ListSearchExtenderPrompt"
                                        PromptText="Type to search" PromptPosition="Top"  IsSorted="true">
                                    </cc1:ListSearchExtender>--%>
                                    <asp:ListBox ID="LsbLevel1" runat="server" Height="410px" Width="173px" onKeyUp="listboxTest(this)"
                                        TabIndex="1" onDblClick="doubleClick(this)"></asp:ListBox>
                                </td>
                                <td>
                                    <%-- <cc1:ListSearchExtender ID="LstSearchExtLevel2" QueryPattern="Contains" TargetControlID="LsbLevel2" runat="server" PromptCssClass="ListSearchExtenderPrompt"
                                        PromptText="Type to search" PromptPosition="Top">
                                    </cc1:ListSearchExtender>--%>
                                    <asp:ListBox ID="LsbLevel2" runat="server" QueryPattern="Contains" Height="410px"
                                        Width="173px" onKeyUp="listboxTest(this)" TabIndex="2" onDblClick="doubleClick(this)">
                                    </asp:ListBox>
                                </td>
                                <td>
                                    <%-- <cc1:ListSearchExtender ID="LstSearchExtLevel3" QueryPattern="Contains" TargetControlID="LsbLevel3" runat="server" PromptCssClass="ListSearchExtenderPrompt"
                                        PromptText="Type to search" PromptPosition="Top">
                                    </cc1:ListSearchExtender>--%>
                                    <asp:ListBox ID="LsbLevel3" runat="server" Height="410px" Width="173px" onKeyUp="listboxTest(this)"
                                        TabIndex="3" onDblClick="doubleClick(this)"></asp:ListBox>
                                </td>
                                <td>
                                    <%--                                    <cc1:ListSearchExtender ID="LstSearchExtLevel4" QueryPattern="Contains" TargetControlID="LsbLevel4" runat="server" PromptCssClass="ListSearchExtenderPrompt"
                                        PromptText="Type to search" PromptPosition="Top">
                                    </cc1:ListSearchExtender>--%>
                                    <asp:ListBox ID="LsbLevel4" runat="server" Height="410px" Width="173px" onKeyUp="listboxTest(this)"
                                        TabIndex="4" onDblClick="doubleClick(this)"></asp:ListBox>
                                </td>
                                <td>
                                    <%--  <cc1:ListSearchExtender ID="LstSearchExtLevel5" QueryPattern="Contains" TargetControlID="LsbLevel5" runat="server" PromptCssClass="ListSearchExtenderPrompt"
                                        PromptText="Type to search" PromptPosition="Top">
                                    </cc1:ListSearchExtender>--%>
                                    <asp:ListBox ID="LsbLevel5" runat="server" Height="410px" Width="173px" onKeyUp="listboxTest(this)"
                                        TabIndex="5" onDblClick="doubleClick(this)"></asp:ListBox>
                                </td>
                                <td>
                                    <%-- <cc1:ListSearchExtender ID="LstSearchExtLevel6"  QueryPattern="Contains" TargetControlID="LsbLevel6" runat="server" PromptCssClass="ListSearchExtenderPrompt"
                                        PromptText="Type to search" PromptPosition="Top">
                                    </cc1:ListSearchExtender>--%>
                                    <asp:ListBox ID="LsbLevel6" runat="server" Height="410px" Width="173px" onKeyUp="listboxTest(this)"
                                        TabIndex="6" onDblClick="doubleClick(this)"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: #FFFF99">
                                    <asp:Button ID="btnAddModule" runat="server" Text="Add Module" Width="100px" TabIndex="7"
                                        OnClick="btnAddModule_Click" />
                                </td>
                                <td align="center" colspan="6" style="background-color: #316391">
                                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" TabIndex="8" Text="Add parameter"
                                        Width="105px" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" TabIndex="9" Text="Edit parameter"
                                        Width="105px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="return confirmforaction('Deletion')"
                                        TabIndex="10" Text="Del parameter" Width="105px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" TabIndex="11"
                                        Text="Export to Excel" Width="105px" />
                                </td>
                            </tr>
                        </table>
                        <div id="divaddEdit" style="border: 1px solid Black; display: none; position: absolute;
                            left: 33%; top: 18%; z-index: 2; color: black; background-color: #FFFFFF;">
                            <center>
                                <table cellpadding="1" cellspacing="1" style="height: 350px; width: 480px">
                                    <tr>
                                        <td colspan="2" style="background-color: #CEE3F6">
                                            <asp:Label ID="Label1" runat="server" ForeColor="black" Font-Bold="true" Text="Add/Edit Parameters"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="trModule" runat="server">
                                        <td style="background-color: #EEEFFD; font-size: 11px; color: #000000; font-weight: 700;">
                                            Module
                                        </td>
                                        <td align="left" style="background-color: #EEEFFD; font-size: 11px; color: #FFFFFF">
                                            <asp:TextBox ID="txtModule" ReadOnly="true" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtCode" Visible="false" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtParentType" Visible="false" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #EEEFFD; font-size: 11px; color: #000000; font-weight: 700;">
                                            Parent
                                        </td>
                                        <td align="left" style="background-color: #EEEFFD; font-size: 11px;">
                                            <asp:TextBox ID="txtParent" ReadOnly="true" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #EEEFFD; font-size: 11px; color: #000000; font-weight: 700;">
                                            Name
                                        </td>
                                        <td align="left" style="background-color: #EEEFFD; font-size: 11px; color: #FFFFFF">
                                            <asp:TextBox ID="txtName" Width="300px" runat="server" CssClass="textbox-css"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #EEEFFD; font-size: 11px; color: #000000; font-weight: 700;">
                                            Description
                                        </td>
                                        <td style="background-color: #EEEFFD; font-size: 11px; color: #FFFFFF">
                                            <asp:TextBox ID="txtDescription" TextMode="MultiLine" Height="200px" Width="400px"
                                                runat="server" CssClass="textbox-css"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #EEEFFD; font-size: 11px; color: #000000; font-weight: 700;">
                                            Active
                                        </td>
                                        <td align="left" style="background-color: #EEEFFD; font-size: 11px; font-weight: 700;">
                                            <asp:RadioButton ID="rbActiveYes" runat="server" Text="Yes" GroupName="active" />
                                            <asp:RadioButton ID="RbActiveNo" runat="server" Text="No" GroupName="active" />
                                        </td>
                                    </tr>
                                    <tr style="background-color: #EEEFFD; font-size: 11px; color: #FFFFFF">
                                        <td align="center" colspan="2">
                                            <asp:Button ID="btnsaveRecords" runat="server" Text="Ok" Width="60px" OnClick="btnsaveRecords_Click"
                                                OnClientClick="return validateDivContent()" />
                                            <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClientClick="return removeDialog()"
                                                Width="60px" OnClick="btncancel_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </div>
                        <div id="divAddEditModule" style="border: 1px solid Black; display: none; position: absolute;
                            left: 33%; top: 18%; z-index: 2; color: black; background-color: #FFFFFF;">
                            <center>
                                <table cellpadding="1" cellspacing="1" style="height: 150px; width: 300px">
                                    <tr>
                                        <td colspan="2" style="background-color: #CEE3F6">
                                            <asp:Label ID="Label2" runat="server" ForeColor="black" Font-Bold="true" Text="Add New Module"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="tr1" runat="server">
                                        <td style="background-color: #EEEFFD; font-size: 11px; color: #000000; font-weight: 700;">
                                            Module Name
                                        </td>
                                        <td style="background-color: #EEEFFD; font-size: 11px; color: #000000; font-weight: 700;"
                                            align="left">
                                            <asp:TextBox ID="txtModulename" Width="200px" CssClass="textbox-css" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #EEEFFD; font-size: 11px; color: #000000; font-weight: 700;"
                                            align="left">
                                            Parent
                                        </td>
                                        <td style="background-color: #EEEFFD; font-size: 11px; color: #000000; font-weight: 700;"
                                            align="left">
                                            <asp:DropDownList ID="ddlModuleTable" CssClass="dropdown-css" Width="200px" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #EEEFFD; font-size: 11px; color: #000000; font-weight: 700;"
                                            align="left">
                                            Active
                                        </td>
                                        <td style="background-color: #EEEFFD; font-size: 11px; color: #000000; font-weight: 700;"
                                            align="left">
                                            <asp:RadioButton ID="mActiveYes" runat="server" Text="Yes" GroupName="Mactive" Checked="true" />
                                            <asp:RadioButton ID="mActiveNo" runat="server" Text="No" GroupName="Mactive" />
                                        </td>
                                    </tr>
                                    <tr style="background-color: #EEEFFD; font-size: 11px; color: #FFFFFF">
                                        <td colspan="2" align="center">
                                            <asp:Button ID="btnModOK" runat="server" Text="Ok" Width="60px" OnClientClick="return validateDivModuleContent()"
                                                OnClick="btnModOK_Click" />
                                            <asp:Button ID="btnModCancel" runat="server" Text="Cancel" Width="60px" OnClick="btnModCancel_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </div>
                        <script language="javascript" type="text/javascript">
                            function showDialog() {
                                var divId = document.getElementById("divaddEdit");
                                divId.style.display = "block";
                                return false;
                            }
                            function showModuleDialog() {
                                var divId = document.getElementById("divAddEditModule");
                                divId.style.display = "block";
                                return false;
                            }
                            function removeModuleDialog() {
                                var divId = document.getElementById("divAddEditModule");
                                divId.style.display = "none";
                                return true;
                            }
                            function removeDialog() {
                                var divId = document.getElementById("divaddEdit");
                                divId.style.display = "none";
                                return true;
                            }
                            function confirmforaction(command) {
                                var agree = confirm('Are you sure you wish to continue ' + command + '?');
                                if (agree)
                                    return true;
                                else
                                    return false;
                            }

                            function validateDivModuleContent() {


                                var id = document.getElementById('ctl00_MainContent_txtModulename');

                                var ModuleName = document.getElementById("ctl00_MainContent_txtModulename").value;

                                if (ModuleName == "") {
                                    alert("plz enter Module name field");
                                    return false;
                                }
                                return true;
                            }


                            function validateDivContent() {
                                //debugger;
                                //ctl00$MainContent$txtDescription

                                var id = document.getElementById('ctl00_MainContent_txtName');
                                var description = document.getElementById('ctl00_MainContent_txtDescription').value;

                                if (id.value.trim() == "") {
                                    alert("Please enter name field.");
                                    return false;
                                }

                                if (description == "") {
                                    alert("Please enter description field.");
                                    return false;
                                }

                                return true;
                            }
                            function listboxTest(id) {

                                var code;
                                var e = window.event;
                                if (e.keyCode)
                                    code = e.keyCode;
                                else if (e.which)
                                    code = e.which;
                                switch (code) {
                                    case 13:

                                        if (id.id == "main")
                                            return false;
                                        // debugger;
                                        if ((id.children.length > 0) && (id.selectedIndex !== -1))
                                            postbackListbox(id);

                                        break;
                                    default:
                                        return;
                                }
                                return;
                            }

                            function postbackListbox(id) {
                                //debugger;
                                var namearr = id.name.split('$');
                                var postBackstr = "__doPostBack('" + id.name + "','" + namearr[namearr.length - 1] + "_SelectedIndexChanged')";
                                window.setTimeout(postBackstr, 0, 'JavaScript');
                                id.focus();
                            }


                            function doubleClick(id) {
                                //debugger;
                                if (id.id == "main")
                                    return false;
                                if ((id.children.length > 0) && (id.selectedIndex !== -1))
                                    postbackListbox(id);
                            }     
       
        
                        </script>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </center>
    </asp:Panel>
</asp:Content>
