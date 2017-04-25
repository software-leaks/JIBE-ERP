<%@ Page Title="Faq" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LMS_FAQ_Builder.aspx.cs" Inherits="LMS_LMS_FAQ_Builder" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="eluc" TagName="Custom" Src="~/UserControl/UserControlEditor.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.js">
    </script>
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
        function LoadRecordInfo(FaqId) {
            var wh = 'FAQ_ID=' + FaqId;

            Get_Record_Information_Details('LMS_DTL_FAQ', wh);
        }
    </script>
    <script type="text/javascript">
        function Checkokclick () {
         $("#dialog-confirm").dialog({
             resizable: false,
             height: 140,
             modal: true,
             close: ReloadPage,
             buttons: {
                 "ok": function () {
                     $(this).dialog("close");
                 },
                 Cancel: function () {
                     $(this).dialog("close");
                 }
             }
         });
     };
     function ReloadPage() {
         window.location.reload(true);
     }
  </script>  
 <%--  <script type="text/javascript">
       var popup;
       function ShowPopup(url) {
           popup = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no,statusbar=no,menubar=no,resizable=0,width=100,height=100,left = 490,top = 262");
           popup.focus();
       }
    </script>
--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        <asp:Label ID="lblPageTitle" runat="server" Text="FAQ"></asp:Label>
    </div>
    <div class="page-content-main">
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 40%; top: 30px; z-index: 2;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <table style="width: 100%" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td style="text-align: left; width: 10%;">
                    Question :
                </td>
                <td style="text-align: left; width: 90%;">
                       <asp:TextBox ID="txtquestion" runat="server" TextMode="MultiLine" Width="99%" Height="20px" Style="text-align: left"></asp:TextBox>
                  
                            <asp:RegularExpressionValidator ID="regexpDesc" runat="server" ErrorMessage="Maximum of 2000 characters allowed"
                             ControlToValidate="txtquestion" Display="Dynamic" ValidationExpression=".{0,2000}"
                             ValidationGroup="Submit"/>
                </td>
            </tr>
            <tr>
                <td>
                    Answer :
                </td>
                <td>
                    <eluc:Custom ID="txtProcedureSectionDetails" runat="server" Width="100%" Height="500px"
                        TextOnly="true" DesgMode="true" HTMLMode="true" PrevMode="true" PictureButton="true"
                        OnFileUploadEvent="btnInsertPic_Click" />
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:UpdatePanel ID="updSaveFAQ" runat="server">
                        <ContentTemplate>
                            <table style="white-space:nowrap">
                                <tr>
                                    <td style="text-align: left; width: 50px">
                                        Office :
                                    </td>
                                    <td style="text-align: left; width: 10px">
                                        <asp:CheckBox ID="chkoffice" Checked="true" runat="server" />
                                    </td>
                                    <td style="text-align: left; width: 50px">
                                        Vessels :
                                    </td>
                                    <td style="text-align: left; width: 10px">
                                        <asp:CheckBox ID="chkvessel" runat="server" />
                                    </td>
                                    <td style="text-align: left; width: 30px">
                                        Module :
                                    </td>
                                    <td style="text-align: left; width: 150px">
                                        <asp:DropDownList ID="ddlModule" Width="150px" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlModule_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left; width: 30px">
                                        Topic :
                                    </td>
                                    <td style="text-align: left; width: 150px">
                                        <asp:DropDownList ID="ddlTopic" Width="150px" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSave_FAQ" runat="server" Text="Save"  
                                            ValidationGroup="Submit" OnClick="btnSave_FAQ_Click" style="height: 21px" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_Update_FAQ" runat="server" Visible="false" Text="Update" OnClick="btnSave_FAQ_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnPublishToCrew" runat="server" Text="Publish To Crew Trainee List"
                                            OnClick="btnPublishToCrew_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAttach" runat="server" Text="Attach Item To FAQ" Visible="false"
                                            OnClick="btnAttach_Click" />
                                    </td>
                                    <td>
                                    <input type="button" id="btnCloseFAQ" title="Close" onclick="window.close();" value="Close" style="width: 90px"/>
                               <%--    <input type="button" value="Show Popup" onclick="ShowPopup('LMS_Faq_List.aspx')" />--%>
                                    </td>
                               
                                </tr>
                                
                            </table>
                            <asp:HiddenField ID="hdnFaq_Id" runat="server" />
                            <asp:HiddenField ID="hdnAttacment_Id" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 1px solid gray;
            background-color: #FDFDFD">
        </div>
    </div>
  
</asp:Content>
