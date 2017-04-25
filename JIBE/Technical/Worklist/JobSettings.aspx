<%@ Page Title="Technical Settings" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="JobSettings.aspx.cs" Inherits="Technical_Worklist_JobSettings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function validation() {
            return confirm("Are you sure ? Modify the value, Then select 'OK'");
        }

        //            function check_field(){
        //                if (document.getElementById($('[id$=txtSettingValue1]').attr('id')) != null) 
        //                {
        //                    var field = document.getElementById($('[id$=txtSettingValue1]').attr('id')).value;

        //                    if (isNaN(field.value)) 
        //                    {
        //                        alert('Please enter only numeric value');
        //                        document.getElementById($('[id$=txtSettingValue1]').attr('id')).value = "";
        //                        return false;
        //                    }
        //                }

        //                if (document.getElementById($('[id$=txtSettingValue2]').attr('id')) != null)
        //                 {
        //                     var field = document.getElementById($('[id$=txtSettingValue2]').attr('id')).value;

        //                    if (isNaN(field.value)) 
        //                    {
        //                        alert('Please enter only numeric value');
        //                        document.getElementById($('[id$=txtSettingValue2]').attr('id')).value = "";
        //                        return false;
        //                    }
        //                }
        //                return true;
        //            }   
        function fnAllowNumeric() {
            if ((event.keyCode < 48 || event.keyCode > 57) && event.keyCode != 8) {
                event.keyCode = 0;
                alert("Please enter only numeric value.");
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 800px;
            height: 100%;">
            <div class="page-title">
                Technical Settings
            </div>
            <div style="height: 650px; width: 800px; color: Black;">
                <asp:UpdatePanel ID="UpdUserType" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 15%">
                                    </td>
                                    <td align="left" style="width: 30%">
                                    </td>
                                    <td align="center" style="width: 5%">
                                        &nbsp;
                                    </td>
                                    <td align="center" style="width: 5%">
                                        &nbsp;
                                    </td>
                                    <td align="center" style="width: 5%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 5%">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divAddSetting" runat="Server">
                        <asp:Table ID="tblJobSetting" runat="server" CssClass="gridmain-css" Width="646px"
                            CellSpacing="2">
                            <asp:TableRow CssClass="RowStyle-css">
                                <asp:TableHeaderCell CssClass="HeaderStyle-css" Height="20%" Width="50%">Settings</asp:TableHeaderCell>
                                <asp:TableHeaderCell CssClass="HeaderStyle-css" Height="20%" Width="50%">Settings Value</asp:TableHeaderCell>
                            </asp:TableRow>
                            <asp:TableRow CssClass="RowStyle-css" ID="trVisible1" runat="server" Visible="false">
                                <asp:TableCell>
                                    <asp:Label ID="lblDescriptionJob" runat="server"></asp:Label></asp:TableCell>
                                <asp:TableCell>
                                    <asp:CheckBox ID="chkbSettingValue" runat="server" />    
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow CssClass="RowStyle-css" ID="trVisible2" runat="server" Visible="false">
                                <asp:TableCell>
                                    <asp:Label ID="lblDescription7Day" runat="server"></asp:Label></asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="txtSettingValue7Day" runat="server" MaxLength="5" onkeypress="return fnAllowNumeric()"></asp:TextBox></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow CssClass="RowStyle-css" ID="trVisible3" runat="server" Visible="false">
                                <asp:TableCell>
                                    <asp:Label ID="lblDescriptionMonth" runat="server"></asp:Label></asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="txtSettingValueMonth" runat="server" MaxLength="5" onkeypress="return fnAllowNumeric()"></asp:TextBox></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                        <br />
                        <asp:Button ID="addnewSetting" runat="server" Text="Save" OnClick="addnewSetting_Click" />
                      </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
