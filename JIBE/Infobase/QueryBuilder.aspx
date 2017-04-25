<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryBuilder.aspx.cs" Inherits="Infobase_QueryBuilder" %>

<%@ Register Src="../UserControl/WebUserControlLogin.ascx" TagName="WebUserControlLogin2"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Query Builder</title>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/modalpopup.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.layout-latest.js" type="text/javascript"></script>
    <script type="text/javascript">

        var myLayout; // a var is required because this page utilizes: myLayout.allowOverflow() method

        $(document).ready(function () {
            //            myLayout = $('body').layout({
            //                west__showOverflowOnHover: true
            //            });

            myLayout = $('body').layout({

                //	reference only - these options are NOT required because 'true' is the default
                closable: true	// pane can open & close
		        , resizable: true	// when open, pane can be resized 
		        , slidable: true	// when closed, pane can 'slide' open over other panes - closes on mouse-out
		        , livePaneResizing: true

                //	some pane-size settings
                , west__size: 400
		        , west__minSize: 300
		        , west__resizable: true
                , east__size: 600
		        , east__minSize: 600
		        , center__minWidth: 100
                , south__size: 30
		        , south__minSize: 30
		        , south__resizable: true

		        , west__animatePaneSizing: false
		        , west__fxSpeed_open: 500	// 1-second animation when opening west-pane
                , west__fxSpeed_close: 500	// 1-second animation when opening west-pane
		        , west__fxName_close: "none"	// NO animation when closing west-pane

                //	enable state management
                //, stateManagement__enabled: true // automatic cookie load & save enabled by default
		        , showDebugMessages: true // log and/or display messages from debugging & testing code
            });

            initScripts();
        });

        function initScripts() {
            $('#maximize-icon').toggle(function () {
                maximize();
            }, function () {
                restore();
            });

        }
        function maximize() {
            myLayout.close('west');
            myLayout.close('east');
            myLayout.close('north');
            myLayout.close('south');

        }
        function restore() {
            myLayout.open('west');
            myLayout.open('east');
            myLayout.open('north');
            myLayout.open('south');
        }


        function validation() {
            if (document.getElementById("ddlObjectType").value == "0") {
                alert("Please Select Object Type.");
                document.getElementById("ddlObjectType").focus();
                return false;
            }
            else if (document.getElementById("ddlSavedQuery").value == "0") {
                alert("Please Select Object .");
                document.getElementById("ddlSavedQuery").focus();
                return false;
            }

            if (document.getElementById("ddlObjectType").value == "TB") {

                if (document.getElementById("ddlFields").value == "0") {
                    alert("Please Select Key Field.");
                    document.getElementById("ddlFields").focus();
                    return false;
                }
            }
            if (document.getElementById("txtDisplayName").value == "") {
                alert("Please Add Display name.");
                document.getElementById("txtDisplayName").focus();
                return false;
            }
            return true;
        }

        function ValidateCatalog() {
            if (document.getElementById("ddlCatalog").value == "0") {
                alert("Please Select a catalog.");
                document.getElementById("ddlCatalog").focus();
                return false;

            }
            return true;
        }
        
    </script>
    <style type="text/css">
        body
        {
            font-family: Tahoma, sans-serif;
            font-size: 0.85em;
        }
        .text-editor
        {
            font-family: Tahoma, sans-serif;
            font-size: 14px;
            margin-top: 5px;
            width: 98%;
            height: 90%;
            border: 0px;
            overflow: auto;
            color: #333;
            min-height: 700px;
        }
        .ui-layout-pane
        {
            /* all 'panes' */
            background: #FFF;
            border: 1px solid #BBB;
            padding: 5px;
            overflow: auto;
        }
        
        .ui-layout-resizer
        {
            /* all 'resizer-bars' */
            background: #E0E6F8;
        }
        
        .ui-layout-toggler
        {
            /* all 'toggler-buttons' */
            background: #000;
            background-image: url("../images/resizer.png");
        }
        
        
        p
        {
            margin: 1em 0;
        }
        
        /*
	 *	Rules below are for simulated drop-down/pop-up lists
	 */
        
        ul
        {
            /* rules common to BOTH inner and outer UL */
            z-index: 100000;
            margin: 1ex 0;
            padding: 0;
            list-style: none;
            cursor: pointer;
            border: 1px solid Black; /* rules for outer UL only */
            width: 15ex;
            position: relative;
        }
        ul li
        {
            background-color: #EEE;
            padding: 0.15em 1em 0.3em 5px;
        }
        ul ul
        {
            display: none;
            position: absolute;
            width: 100%;
            left: -1px; /* Pop-Up */
            bottom: 0;
            margin: 0;
            margin-bottom: 1.55em;
        }
        .ui-layout-north ul ul
        {
            /* Drop-Down */
            bottom: auto;
            margin: 0;
            margin-top: 1.45em;
        }
        ul ul li
        {
            padding: 3px 1em 3px 5px;
        }
        ul ul li:hover
        {
            background-color: #FF9;
        }
        ul li:hover ul
        {
            display: block;
            background-color: #EEE;
        }
        #dvResult
        {
            font-size: 10px;
            margin: 10px;
        }
        .toolbar div
        {
            padding: 5px;
            text-align: right;
        }
        .toolbox
        {
            margin-top: 5px;
            padding: 10px;
            border: 1px solid #c1c1dc;
            background-color: #efefdc;
        }
        .toolbox-header
        {
            margin-top: 10px;
            font-size: 12px;
            font-weight:bold;
            padding: 4px;
            text-align: center;
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
    </style>
</head>
<body>
    <form id="frmMaster" runat="server">
    <asp:ScriptManager ID="src" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="divProgress" style="background-color: transparent; position: absolute; left: 49%;
                top: 20px; z-index: 2; color: black">
                <img src="../images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="ui-layout-north" onmouseover="myLayout.allowOverflow('north')" onmouseout="myLayout.resetOverflow(this)"
        style="padding: 0px;">
        <div class="header">
            <div class="title">
                <asp:Image ID="Image1" ImageUrl="~/Images/app_logo.png" runat="server" Style="vertical-align: bottom;" />
            </div>
            <div class="loginDisplay">
            </div>
            <div class="clear hideSkiplink">
            </div>
        </div>
    </div>
    <div class="ui-layout-west">
        <asp:UpdatePanel ID="UpdatePanel_west" runat="server">
            <ContentTemplate>
                <div class="gradiant-css-orange" style="font-size: 14px; padding: 4px; text-align: center;">
                    <div style="float: right">
                        <img src="../images/Pin_Off.png" onclick="myLayout.toggle('west')" />
                    </div>
                    Database
                </div>
                <div class="toolbox">
                    <asp:Label ID="lblLoginInfo" runat="server"></asp:Label>
                </div>
                <div class="toolbox">
                    <asp:HiddenField ID="hdnServer" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdnCatalog" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdnUsername" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdnPassword" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdnQueryID" runat="server"></asp:HiddenField>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                Server
                            </td>
                            <td style="vertical-align: top">
                                <asp:DropDownList ID="ddlServer" runat="server" OnSelectedIndexChanged="ddlServer_SelectedIndexChanged"
                                    Width="146px" Height="22px" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:ImageButton ID="btnSearchServer" runat="server" OnClick="btnSearchServer_Click"
                                    ImageUrl="~/Images/reload.png" ImageAlign="AbsBottom" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Login
                            </td>
                            <td>
                                <asp:TextBox ID="txtLogin" runat="server" Width="160px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Password
                            </td>
                            <td>
                                <asp:TextBox ID="txtPassword" runat="server" Width="160px" TextMode="Password" ViewStateMode="Enabled"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <asp:Button ID="btnConnect" runat="server" Width="80px" Text="Connect" OnClick="btnConnect_Click" />
                                <asp:Label ID="lblConnect" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Catalog
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCatalog" runat="server" Width="200px">
                                </asp:DropDownList>
                                <asp:Button ID="btnLogin" runat="server" Width="80px" Text="LogIn" OnClientClick="return ValidateCatalog();" OnClick="btnLogin_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                            </td>
                        </tr>
                    </table>
                </div>
                 <div class="toolbox-header" >
                    Object Details
                </div>
                <div class="toolbox">
                    <asp:Panel ID="Panel1" runat="server" Enabled="true">
                        <asp:Label ID="lblType" Text="Object Type:" runat="server"></asp:Label> 
                        <asp:DropDownList ID="ddlObjectType" runat="server" Width="150px" Height="22px" OnSelectedIndexChanged="ddlObjectType_SelectedIndexChanged"
                            AutoPostBack="true">
                             <asp:ListItem Text="--Select Type--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Stored Proc" Value="SP"></asp:ListItem>
                            <asp:ListItem Text="Table" Value="TB"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:Panel>
                </div>
                <div class="toolbox">
                    <asp:Panel ID="pnlSavedProc" runat="server" Enabled="false">
                                            <table>
                                            <td>
                         <asp:Label ID="lblObject" Text="Select Object:" runat="server"></asp:Label></td>
                         <td>
                        <asp:DropDownList ID="ddlSavedQuery" runat="server" Width="150px" Height="22px" OnSelectedIndexChanged="ddlSavedQuery_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:ImageButton ID="btnReloadSavedProcedures" runat="server" OnClick="btnReloadSavedProcedures_Click"
                            ImageUrl="~/Images/reload.png" ImageAlign="AbsBottom" />
                        </td>
                        <tr>
                        <td>

                        <asp:Label ID="lblDisplayName" Text="Display Name :" runat="server"></asp:Label>
                        </td>
                        <td>
                        <asp:TextBox ID="txtDisplayName" runat="server" MaxLength="200"></asp:TextBox>
                       </td>
                        </tr>
                        <tr>
                        <td>
                         <asp:Label ID="lblKeyField" Text="Select Key Field:" runat="server"></asp:Label> 
                        </td>
                        <td>
                         <asp:DropDownList ID="ddlFields" runat="server" Width="250px" 
                            AutoPostBack="true" SelectionMode="Single"></asp:DropDownList>
                        </td>
                        </tr>
                        <tr>
                        <td colspan="2" align="center">
                         <asp:Button ID="btnSave" Text="Save Object" runat="server"  OnClientClick="return validation();" onclick="btnSave_Click" />
                        </td>
                        </tr>
                        <tr>
                        <td colspan="2" style="color:Green"></td>
                        <asp:Label ID="lblSuccess" Text="Saved successfully." runat="server" Visible="false"></asp:Label>
                        </tr>
                        </table>
                        </asp:Panel>
                        </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="ui-layout-south">
    </div>
    <div class="ui-layout-east">
        <asp:UpdatePanel ID="UpdatePanel_East" runat="server">
            <ContentTemplate>
                <div class="gradiant-css-orange" style="font-size: 14px; padding: 4px; text-align: center">
                    <div style="float: left">
                        <img src="../Images/Pin_Off_right.png" onclick="myLayout.toggle('east')" />
                    </div>
                    <span>Preview</span>
                </div>
                <div id="dvResult">
                    <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="true">
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="ui-layout-center" style="text-align: center; background-color: Aqua;">
        <asp:UpdatePanel ID="UpdatePanel_Center" runat="server">
            <ContentTemplate>
                <div class="gradiant-css-orange" style="font-size: 14px; padding: 4px">
                    <div style="float: right">
                        <img src="../images/xf_maximize_icon.png" id="maximize-icon" />
                    </div>
                    <img src="../images/wizard/database-process-icon.png" style="vertical-align: bottom"
                        height="20px" />
                    &nbsp;&nbsp; SQL Query Builder</div>
                <div id="dvToolBar" class="toolbar">
                    <div style="float: left; text-align: left;">
                        <asp:Label ID="lblQueryDetails" runat="server"></asp:Label>
                    </div>
                    <asp:Panel ID="pnlCtl" runat="server" Visible="false">
                        <asp:Button ID="btnSaveQueryAs" runat="server" Text="Save Query As" BorderStyle="Solid" OnClientClick="return ValidateCatalog();"
                            OnClick="btnSaveQueryAs_Click" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                            Height="24px" BackColor="#81DAF5" />
                        <asp:Button ID="btnSaveQuery" runat="server" Text="Save Query" BorderStyle="Solid" OnClientClick="return ValidateCatalog();"
                            OnClick="btnSaveQuery_Click" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                            Height="24px" BackColor="#81DAF5" />
                        <asp:Button ID="btnPreview" runat="server" Text="Preview Result" BorderStyle="Solid"
                            OnClick="btnPreview_Click" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                            Height="24px" BackColor="#81DAF5" />
                    </asp:Panel>
                </div>
                <asp:TextBox ID="txtQuery" runat="server" TextMode="MultiLine" CssClass="text-editor" Enabled="false"
                    BorderStyle="Solid" BorderWidth="1" BorderColor="#dcdcdc"></asp:TextBox>
                <asp:Label ID="lblCreatedBy" runat="server"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvSaveCommand" title="Save Query" style="border: 1px solid #efefef; padding: 10px;
        margin-top: 2px; display: none; width: 400px;">
        <asp:UpdatePanel ID="UpdatePanel_Command" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td>
                            Command Name
                        </td>
                        <td>
                            <asp:TextBox ID="txtQueryName" runat="server" Width="260px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Command Type
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCommandType" runat="server" Width="260px">
                                <asp:ListItem Text="InfoBase SP" Value="InfoSP"></asp:ListItem>

                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Result Type
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlResultType" runat="server" Width="260px">
                                <asp:ListItem Text="Single Table" Value="Single Table"></asp:ListItem>
                                <asp:ListItem Text="Multiple Tables" Value="Multiple Tables"></asp:ListItem>
                                <asp:ListItem Text="Integer" Value="Integer"></asp:ListItem>
                                <asp:ListItem Text="String" Value="String"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnSaveCommand" runat="server" Width="80px" Text="Save" OnClick="btnSaveCommand_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
