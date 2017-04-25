<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProcedureBuilder.aspx.cs"
    EnableEventValidation="false" Inherits="ProcedureBuilder" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="eluc" TagName="Custom" Src="~/UserControl/UserControlEditor.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Procedure Builder</title>
    <script type="text/javascript" language="javascript" src="JS/js_globals.aspx"></script>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/modalpopup.js" type="text/javascript"></script>
    <script src="JS/jquery.layout.js" type="text/javascript"></script>
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
                , west__size: 350
		        , west__minSize: 350
		        , west__resizable: true
                , east__size: 800
		        , east__minSize: 800
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
                //preview();
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
        function preview() {
            myLayout.open('west');
            myLayout.open('east');
            myLayout.open('north');
            myLayout.open('south');
            myLayout.close('center');
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
            background-image: url("../../images/resizer.png");
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
            padding: 4px;
            text-align: center;
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
        
        .RowStyle-css
        {
            font-size: 11px;
            background-color: white;
            color: #333333;
        }
        .section-row
        {
            padding-left: 3px;
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
                <asp:Image ID="Image1" ImageUrl="../Images/app_logo.png" runat="server" Style="vertical-align: bottom;" />
            </div>
            <div class="clear hideSkiplink">
            </div>
        </div>
    </div>
    <div class="ui-layout-west">
        <div class="gradiant-css-orange" style="font-size: 14px; padding: 4px; text-align: center;">
            <div style="float: right">
                <img src="../images/Pin_Off.png" onclick="myLayout.toggle('west')" alt="" />
            </div>
            Procedure
        </div>
        <div class="toolbox-header" style="text-align: left">
            <asp:Panel ID="pnlCtl" runat="server" Visible="true">
                <asp:Button ID="btnPublishProcedure" runat="server" Text="Publish Procedure" BorderStyle="Solid"
                    OnClick="btnPublishProcedure_Click" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                    Height="22px" BackColor="#81DAF5" />
                <asp:Button ID="btnSendForApproval" runat="server" Text="Send for Appoval" BorderStyle="Solid"
                    OnClick="btnSendTo_Click" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                    Height="22px" BackColor="#81DAF5" />
            </asp:Panel>
        </div>
        <div class="toolbox">
            <div style="text-align: left; width: 100%; overflow: auto; z-index: 1; border: 1px solid inset;">
                <table style="width: 100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="vertical-align: middle; text-align: left; color: Black; font-size: 11px;
                            font-weight: bold; padding: 0px 0px 10px 0px;">
                            <table width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td style="text-align: right;">
                                        Code :
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtProcedureCode" runat="server" Width="245px" BackColor="Transparent"
                                            BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        Name :
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtProcedureName" Width="245px" BackColor="Transparent" runat="server"
                                            BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align: right; vertical-align: top">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="border: 0px solid gray;">
                            <asp:GridView ID="gvSectionList" DataKeyNames="Section_ID" runat="server" GridLines="None"
                                ShowHeader="false" Width="100%" CellPadding="2" CellSpacing="0" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="Header" ItemStyle-CssClass="section-row">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSection_Header" Width="220" Height="25px" MaxLength="200" runat="server"
                                                Text='<%#Eval("SECTION_HEADER") %>' BorderStyle="Solid" BorderWidth="1px" BorderColor="#FCFCFC"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rqvtxtSection_Header" ValidationGroup="vgeditSection"
                                                runat="server" ControlToValidate="txtSection_Header" Display="Dynamic" ErrorMessage="Please enter section name"></asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnSectionDelete" runat="server" OnCommand="btnSectionDelete_Command"
                                                ToolTip="Delete" Height="16px" Width="16px" CommandArgument='<%#Eval("Section_ID")%>'
                                                OnClientClick="var c= confirm('Are you sure to delete ?'); if(c) return true ; else return false"
                                                ImageUrl="~/Images/Delete.png" ImageAlign="AbsBottom" AlternateText="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnSectionDown" ImageUrl="~/Images/Arrow2 - Down.png" AlternateText="Down"
                                                ToolTip="Section Down" Height="15px" Width="15px" CommandArgument="DOWN" ImageAlign="AbsBottom"
                                                OnCommand="btnChangeSectionPosition_Command" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnSectionUp" CommandArgument="UP" ImageUrl="~/Images/Arrow2 - Up.png"
                                                ToolTip="Section Up" Height="15px" Width="15px" AlternateText="UP" ImageAlign="AbsBottom"
                                                OnCommand="btnChangeSectionPosition_Command" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnViewSectionDetails" runat="server" OnCommand="imgbtnViewSectionDetails_Command"
                                                ImageAlign="AbsBottom" AlternateText="View" ImageUrl="~/Images/view-detail-qmsprocedure.png"
                                                ToolTip="View section" CommandArgument='<%#Eval("Section_ID")%>' Visible='<%#Convert.ToString(Eval("Section_ID"))=="0"?false:true %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                <RowStyle CssClass="RowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" BackColor="#FFBA66" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-top: 10px;">
                            <asp:Button ID="btnNewSection" runat="server" ValidationGroup="vgeditSection" OnClick="btnNewSection_Click"
                                Height="22px" BorderStyle="Solid" BorderWidth="1" BorderColor="#0489B1" BackColor="#81DAF5"
                                Text="Add new section" />
                            &nbsp;
                            <asp:Button ID="btnSectionSave" OnCommand="btnSectionSave_Command" Text="Save" Height="22px"
                                BorderStyle="Solid" BorderWidth="1" BorderColor="#0489B1" BackColor="#81DAF5"
                                ValidationGroup="vgeditSection" runat="server" />
                            &nbsp;
                            <asp:Button ID="btnProcedurePreview" OnClick="imgbtnPreviewProdecure_Click" Text="Preview"
                                Height="22px" BorderStyle="Solid" BorderWidth="1" BorderColor="#0489B1" BackColor="#81DAF5"
                                runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanelEditSection" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="ui-layout-south">
    </div>
    <div class="ui-layout-center" style="text-align: center; background-color: White;">
        <asp:UpdatePanel ID="UpdatePanel_Center" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="gradiant-css-orange" style="font-size: 14px; padding: 4px">
                    <div style="float: right">
                        <img src="../images/xf_maximize_icon.png" id="maximize-icon" alt="" />
                    </div>
                    &nbsp;&nbsp;Section Detail
                </div>
                <div id="dvToolBar" class="toolbar">
                    <div style="float: left; text-align: left;">
                        <asp:Label ID="lblQueryDetails" runat="server"></asp:Label>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%-- <asp:UpdatePanel ID="updtxtProcedureSectionDetails" runat="server">
            <ContentTemplate>--%>
        <eluc:Custom ID="txtProcedureSectionDetails" runat="server" Width="100%" Height="500px"
            TextOnly="true" DesgMode="true" HTMLMode="true" PrevMode="true" PictureButton="true"
            OnFileUploadEvent="btnInsertPic_Click" />
        <%--  </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
    <div id="EditSection" title="Add Section" style="border: 1px solid #efefef; padding: 10px;
        margin-top: 2px; width: 600px;">
    </div>
    <div id="DivSendForApp" title="Approval" style="border: 1px solid #efefef; padding: 10px;
        margin-top: 2px; display: none; width: 400px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel2" runat="server" Visible="true">
                    <table cellpadding="2" cellspacing="0" style="border: 1px solid #dcdcdc; width: 100%;">
                        <tr>
                            <td style="vertical-align: top;">
                                <asp:Label ID="lblUser" runat="server" Text="Send To" Enabled="true"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="lstUser" runat="server" Width="230px" SelectionMode="Multiple"
                                    DataTextField="UserName" DataValueField="UserID">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                <asp:Label ID="lblWorkStatus" runat="server" Text="For Work"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="230px">
                                    <asp:ListItem Text="Send For Approval" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Re-Work" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                <asp:Label ID="lblComments" runat="server" Text="Comments"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Rows="5" Width="240px"
                                    Enabled="true"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Button ID="btnSendApproval" runat="server" Text="Send" BorderStyle="Solid" OnClick="btnSendForApproval_Click"
                                    BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma" Height="24px" BackColor="#81DAF5" />
                                <asp:Button ID="btnCancelSend" runat="server" Text="Cancel" BorderStyle="Solid" OnClick="btnCancelApp_Click"
                                    BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma" Height="24px" BackColor="#81DAF5" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="DivApproval" title="Publish Comments" style="border: 1px solid #efefef;
        padding: 10px; margin-top: 2px; display: none; width: 400px;">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server" Visible="true">
                    <table cellpadding="2" cellspacing="0" style="border: 1px solid #dcdcdc; width: 100%;">
                        <tr>
                            <td style="vertical-align: top;">
                                <asp:Label ID="Label3" runat="server" Text="Comments"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtApprovalComments" runat="server" TextMode="MultiLine" Rows="5"
                                    Width="240px" Enabled="true"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Button ID="BtnSaveApproved" runat="server" Text="Publish" BorderStyle="Solid"
                                    OnClick="BtnSaveApproved_Click" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                    Height="24px" BackColor="#81DAF5" />
                                <asp:Button ID="btnCancelPublish" runat="server" Text="Cancel" BorderStyle="Solid"
                                    OnClick="btnCancelPublish_Click" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                    Height="24px" BackColor="#81DAF5" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
