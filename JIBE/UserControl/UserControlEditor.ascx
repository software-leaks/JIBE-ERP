<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControlEditor.ascx.cs"
    Inherits="UserControlEditor" %>
<%@ Register Namespace="SMS.Custom" TagPrefix="Custom" Assembly="SMS.Business.LIB"  %>
  <style type="text/css">
        div.ajax__htmleditor_attachedpopup_default
        {
            border-color: #B0B0B0;
            border-style: solid;
            border-width: 1px;
            font-family: Arial;
            font-size: 11px;
            padding: 5px;
            background-color: #DFDFDF;
            height: 75px;
            width: 258px;                       
        }
        .ajax__htmleditor_attachedpopup_default label, button, div, input, select, td, fieldset
        {
            font-family: Tahoma;
            font-size: 11px;
        }
        .ajax__htmleditor_attachedpopup_default div.ajax__htmleditor_popup_confirmbutton
        {
            float: right;
            margin-left: 5px;
            margin-top: 5px;            
        }
        .ajax__htmleditor_attachedpopup_default div.ajax__htmleditor_popup_boxbutton
        {
            background-color: #C2C2C2;
            border-width: 0;
            margin: 0;
            padding: 1px;
        }
        .ajax__htmleditor_attachedpopup_default .ajax__htmleditor_popup_bgibutton
        {
           
            border-width: 0;
            font-weight: bold;
            height: 19px;
            overflow: hidden;
            text-align: center;
            vertical-align: middle;
            border-color: #B0B0B0;
            border-style: solid;
            border-width: 1px;
            width:60px;
        }
    </style>
     <script language="javascript" type="text/javascript">

         function show(e) {
             document.getElementById("<%=dv.ClientID%>").style.display = "block";            
             showFloatDiv(e);
         }

         function hide() {
             document.getElementById("<%=dv.ClientID%>").style.display = "none";          
         }

         function showFloatDiv(e) {
             if (!e) {
                 e = window.event || arguments.callee.caller.arguments[0];
             }
             var scrolledV = scrollV();
             var scrolledH = (navigator.appName == 'Netscape') ? document.body.scrollLeft : document.body.scrollLeft;
             tempX = (navigator.appName == 'Netscape') ? e.clientX : event.clientX;
             tempY = (navigator.appName == 'Netscape') ? e.clientY : event.clientY;
             var obj = document.getElementById("<%=dv.ClientID%>");
             var x = ((obj.offsetWidth + tempX) - browserWidth());
             obj.style.left = (tempX + scrolledH) - (x > 0 ? x : 0) + 'px';
             obj.style.top = (tempY + scrolledV) + 'px';
             obj.style.display = "block";
         }
         function browserWidth() {
             var wth;
             if (window.innerWidth) {
                 wth = window.innerWidth;
             }
             else if (document.documentElement && document.documentElement.clientWidth) {
                 wth = document.documentElement.clientWidth;
             }
             else if (document.body) {
                 wth = document.body.clientWidth;
             }
             return wth;
         }
         function scrollV() {
             var scrolledV;
             if (window.pageYOffset) {
                 scrolledV = window.pageYOffset;
             }
             else if (document.documentElement && document.documentElement.scrollTop) {
                 scrolledV = document.documentElement.scrollTop;
             }
             else if (document.body) {
                 scrolledV = document.body.scrollTop;
             }
             return scrolledV;
         }
         function validate(e) {
             var imgButton = document.getElementById("<%=btnImageUpload.ClientID%>");
             if (imgButton.value == null || imgButton.value == "") {
                 alert("Select a Image to Upload");
                 return false;
             }
         }
</script>
<Custom:SMSCustomEditor ID="ucCustomEditor" runat="server" Height="100%" Width="100%" />


<div id="dv" class="ajax__htmleditor_attachedpopup_default" style="position:absolute; display: none;" runat="server">
    <table cellspacing="0" cellpadding="0" border="0" style="width: 100%;">
        <tr>
            <td align="left">
                <span>Image :</span>
            </td>
            <td align="left">
                <asp:FileUpload ID="btnImageUpload" runat="server" CssClass="input" />
            </td>
        </tr>
        <tr>
            <td align="center" style="-moz-user-select: none;" colspan="2">
                <table cellspacing="0" cellpadding="3" border="0">
                    <tbody>
                        <tr>
                            <td align="center" valign="middle">
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="ajax__htmleditor_popup_bgibutton" OnClientClick="validate();" OnClick="btnUpload_Click" />
                            </td>
                            <td align="center" valign="middle">
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="hide();" CssClass="ajax__htmleditor_popup_bgibutton" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
</div>
