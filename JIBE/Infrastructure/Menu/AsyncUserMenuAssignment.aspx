<%@ Page Title="User Menu Assignment" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AsyncUserMenuAssignment.aspx.cs" Inherits="Infrastructure_Menu_UserMenuAssignmentNew" %>

<%@ Register Src="../../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/ui.all.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../../Scripts/ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/ui.progressbar.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/paging.js" type="text/javascript"></script>
    <script src="../../Scripts/iframe.js" type="text/javascript"></script>
    <link href="../../Styles/jquery-ui1.11.0.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-ui.min1.11.0.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/AsyncUserAssignTree.js" type="text/javascript"></script>
    <link href="../../jqxWidgets/Controls/styles/jqx.base.css" rel="stylesheet" type="text/css" />
    <%--<script src="../../jqxWidgets/scripts/jquery-1.11.1.min.js" type="text/javascript"></script>--%>
    <script src="../../jqxWidgets/Controls/jqxcore.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxdata.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxbuttons.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxscrollbar.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxpanel.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxtree.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxlistbox.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxdropdownlist.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxcheckbox.js"></script>
    <style type="text/css">
        body, html
        {
            overflow-x: hidden;
        }
        
        .page
        {
            width: 100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }
        
        .page
        {
            width: 100%;
        }
    </style>
    <style type="text/css">
        .listbox
        {
            border: 0px;
        }
        .SelectedNodeStyle
        {
            background: url(../../Images/bg.png) left -1672px repeat-x;
        }
        .pager span
        {
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
            background: url(../../Images/bg.png) left -1672px repeat-x;
            font-size: 14px;
        }
        .pager a
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:link
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:visited
        {
            color: blue;
            background-color: white;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:hover
        {
            color: blue;
            background-color: #efefef;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
    </style>
    <style type="text/css">
        .pg-normal
        {
            color: black;
            font-weight: normal;
            text-decoration: none;
            cursor: pointer;
        }
        .pg-selected
        {
            color: black;
            font-weight: bold;
            text-decoration: underline;
            cursor: pointer;
        }
        
        .ui-widget-header
        {
            border: 1px solid #e78f08;
            background: #3498DB url("images/ui-bg_gloss-wave_35_f6a828_500x100.png") 50% 50% repeat-x;
            color: #ffffff;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">

        var CUserID = '<%= Session["USERID"] %>';
        var UserCompanyID = '<%= Session["USERCOMPANYID"] %>'

        $(document).ready(function () {

            $("#progressbar").progressbar({ value: 0 });
            $("#lbldisp").hide();
            FetchData(CUserID, UserCompanyID);
            //           

        });

        

        

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <center>
            <table width="20%"  style="text-align: center;">
               
                <td style="text-align: center">
                    <div id="progressbar" style=" visibility:hidden;">
                    </div>
                    <div id="result">
                    </div>
               
                </td>
            </table>
            </center>
            <table style="width: 100%" cellspacing="3">
                <tr>
                    <td>
                        Company Name:
                    </td>
                    <td colspan="2">
                        <div id="dvlstCompany" style="text-align: left;">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        Filter User:
                    </td>
                    <td>
                        <div id="dvSearchUser">
                        </div>
                    </td>
                    <td>
                        <div style="border: 1px solid #0489B1; background-color: #A9D0F5; padding: 2px;">
                            <table>
                                <tr>
                                    <td>
                                        <div id="dvRBCopyFrom">
                                        </div>
                                    </td>
                                    <td>
                                        <div id="dvCopyFromUser">
                                        </div>
                                    </td>
                                    <td>
                                        <div id="dvCopyFromRole" style="display: none">
                                        </div>
                                    </td>
                                    <td>
                                        To User:
                                    </td>
                                    <td>
                                        <div>
                                            <div style='float: left;' id='jqxWidget'>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div id="dvAppendMode">
                                        </div>
                                    </td>
                                    <td>
                                        <div id="dvCopy">
                                            <input type="button" id="btnCopyAccess" onclick="CopyMenuAccess();" value="Copy Access" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%; margin-top: 5px;">
                <tr>
                    <td style="vertical-align: top; border: 1px solid #cccccc; width: 15%; min-width: 250px;">
                        <asp:ListBox ID="lstUserList" runat="server" Height="600px" Width="100%" AutoPostBack="false"
                            CssClass="listbox" SelectionMode="Single" onchange="ListBoxSelection();"></asp:ListBox>
                    </td>
                    <td style="vertical-align: top; border: 1px solid #cccccc; width: 15%;">
                        <div id="maintree" style="height: 550px;">
                            <div id="FunctionalTree" style="min-width: 250px; max-height: 590px; padding-top: 5px;">
                            </div>
                        </div>
                    </td>
                    <td style="vertical-align: top; border: 1px solid #cccccc;">
                        <div id="dvMenuGrid">
                        </div>
                        <div id="pageNavPosition">
                        </div>
                        <div id="dvButton" style="border: 1px solid outset; text-align: right;">
                            <input type="button" id="btnResetMenu" onclick="ResetTableMenu();" value="Remove Access" />
                            <input type="button" id="btnSave" onclick="SaveMenuChanges();" value="Save Changes" />
                        </div>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdnUserListValue" runat="Server" Value="" />;
            <asp:HiddenField ID="hdnRows" runat="Server" Value="" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
