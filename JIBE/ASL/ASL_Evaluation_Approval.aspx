<%@ Page Title="Evaluation Approval" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ASL_Evaluation_Approval.aspx.cs" Inherits="ASL_ASL_Evaluation_Approval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
 <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
<script type="text/javascript">
    function CloseWindow() {
//        window.open('', '_self', '');
//        window.close();
        top.window.open('', '_parent', '');
        top.window.close();
    }

</script>
<script type="text/javascript">
    function closeWin() {
        if (navigator.appName == "Microsoft Internet Explorer") {
            window.parent.window.opener = null;
            window.parent.window.close();
        } else if (navigator.appName == "Netscape") {
            top.window.opener = top;
            top.window.open('', '_parent', '');
            top.window.close();
        }
    } 
</script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

</asp:Content>

