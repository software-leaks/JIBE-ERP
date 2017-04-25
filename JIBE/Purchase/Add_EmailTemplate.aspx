<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.master" CodeFile="Add_EmailTemplate.aspx.cs" Inherits="Purchase_Add_EmailTemplate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
   <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../ckeditor/ckeditor.js"></script>
<script type="text/javascript" src="../ckeditor/adapters/jquery.js"></script>

        <script language="javascript" type="text/javascript">

            function validation() {

                var myEditor = CKEDITOR.instances.<%=txtemailbody.ClientID %>;
                   //alert(myEditor.getData().length);
             

                if (document.getElementById("ctl00_MainContent_ddlEmailType").value == "0") {
                    alert("Email Type is mandatory field.");
                    document.getElementById("ctl00_MainContent_ddlEmailType").focus();
                    return false;
                }

                if (document.getElementById("ctl00_MainContent_txtemailsubject").value == "") {
                    alert("Email Subject is mandatory field.");
                    document.getElementById("ctl00_MainContent_txtemailsubject").focus();
                    return false;
                }

                if (myEditor.getData().length == 0) {
                    alert("Email Body is mandatory field.");
                    document.getElementById("ctl00_MainContent_txtemailbody").focus();
                    return false;
                }
               
                return true;
            }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

 <div id="divadd" title='<%# Request.QueryString["OperationMode"] %>' style="font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 100%">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 20%">
                                      Email Type&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlEmailType" runat="server" CssClass="txtInput" Width="400px">
                                                </asp:DropDownList>
                                                
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 20%">
                                      Email Subject&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtemailsubject" MaxLength="500" TextMode="MultiLine" CssClass="txtInput" Width="99%" runat="server"></asp:TextBox>
                          
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 20%">
                                      Email Body&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                   
                                   <CKEditor:CKEditorControl ID="txtemailbody"  Height="100px" Width="90%" CssClass="cke_show_borders" runat="server"></CKEditor:CKEditorControl>
                                        
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center;background-color:#d8d8d8;">
                                       
                                        <asp:Button ID="btnsave" runat="server" Text="Save" 
                                            OnClientClick="return validation();" onclick="btnsave_Click" />
                                        <asp:TextBox ID="txtUserTypeID" runat="server" Visible="false" Width="1px"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center;">

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 0px solid #cccccc;
                                            background-color: #FDFDFD">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="right" style="color: #FF0000; font-size: small;">
                                        * Mandatory fields
                                    </td>
                                </tr>
                            </table>
                        </div>
</asp:Content>
