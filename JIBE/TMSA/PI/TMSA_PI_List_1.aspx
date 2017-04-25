<%@ Page Title=" Jibe: PI Details" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="TMSA_PI_List_1.aspx.cs" Inherits="TMSA_PI_List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cplexdertask" %>

<%@ Register Src="~/UserControl/ucAsyncPager.ascx" TagName="ucAsyncPager" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/Common.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui.min.js"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/Faq_List.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/jsasyncpager.js" type="text/javascript"></script>
    <style type="text/css">
        .TableStyleCSS
        {
            width: 100%;
        }
        
        .QuestionCSS
        {
        }
        .QuestionCSS-Icon
        {
            width: 20px;          
        
        }
        .QuestionCSS-FAQ
        {
            width: 95%;
            padding-left: 10px;
        }
        .QuestionCSS-FAQ a
        {
            font-weight: bold;
            text-decoration: none;
            font-size: 12px;
            color: Black;
        }
        .QuestionCSS td
        {
            padding: 3px;
            background-color: #dcdcdc;
            margin-bottom: 1px;
            border-bottom: 1px solid white;
        }
        
        
        .QuestionCSS-RecordInfo
        {
            padding: 5px;
            float: right;
        }
        .QuestionCSS-RecordInfo img
        {
            height: 16px;
            width: 16px;
            border-width: 0px;
            padding-left: 40;
        }
        .AnswerCSS-Hide
        {
            display: none;
        }
        .ShortAns-Hide
        {
            display: none;
        }
        .AnswerCSS-Show
        {
            display: block;
        }
        
        .AnsDiv
        {
            background-color: #efefef;
            border: 1px solid #cccccc;
            margin-left: 10px;
            padding: 5px;
        }
        
        
        .QuestionCSS-Edit
        {
            text-align: right;
            display: <%=UserAccess_Edit%>;
        }
    </style> 
    <script type="text/javascript">
       $(function () {
           $(':text').bind('keydown', function (e) {
               //on keydown for all textboxes
               if (e.target.className != "searchtextbox") {
                   if (e.keyCode == 13) { //if this is enter key
                 
                       asyncBindFAQListFaq(<%=UserAccess_Edit %>);
                       e.preventDefault();
                       return false;
                   }
                   else
                       return true;
               }
               else
                   return true;
           });
       });

         
    </script>
    <script type="text/javascript">
    $(function () {
        $('#btnclearfilter').click(function () {
            $('#txtSearch').val('');
            asyncBindFAQListFaq(<%=UserAccess_Edit %>);
        })
    })
    </script>
    <script type="text/javascript">
        function RefreshParent() {
            if (window.opener != null && !window.opener.closed) {
                window.opener.location.reload();
            }
        }
        window.onbeforeunload = RefreshParent;
    </script>
    <script type="text/javascript">
        function ExportDivDataToExcel() {
            var html = $("#dvasyncPIList").html();
            html = $.trim(html);
            html = html.replace(/>/g, '&gt;');
            html = html.replace(/</g, '&lt;');
            $("input[id$='HdnValue']").val(html);
        }
</script>     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField ID="hdf_User_ID" runat="server" />
      <div runat="server" id="ExportDiv">
    <div class="page-title">
        <asp:Label ID="lblPageTitle" runat="server" Text=" Jibe: FAQ"></asp:Label>
    </div>
    <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
            color: Black; text-align: left; background-color: #fff;">
        <table id="testTable" width="100%" cellpadding="1" style="margin-bottom: 10px">
            <tr>
                <td>
                    <input id="txtSearch" type="text" style="width: 180px; background-color: #FFFFE6" /><input
                        type="button" id="btnserch" title="Search" onclick="asyncBindFAQList(<%=UserAccess_Edit %>);"
                        value="Search" style="width: 90px" /><input type="button" id="btnclearfilter" value="Clear Filter" width="90px" />
                </td>
                <td>
                    <input type="button" id="btnAddNewPI" title="Add New PI" onclick="window.open('TMSA_PI_Entry.aspx');"
                        value="Add New PI" style="width: 90px"  />
                </td>
                <td>
                 <%--   <input type="submit" value="Export to EXCEL" onclick="write_to_excel();"/>--%>
                      <asp:Button ID="BtnExport" runat="server" onclick="BtnExport_Click" Text="Export to Excel" OnClientClick="ExportDivDataToExcel()"/>
                </td>
            </tr>
        </table>
        <table style="width: 100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div id="dvasyncPIList">
                        loading....
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <uc1:ucAsyncPager ID="ucAsyncPager1" runat="server" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HdnValue" runat="server"/>
        <asp:HiddenField ID="hdnEdit" runat="server"/>
    </div>
    </div>
</asp:Content>
