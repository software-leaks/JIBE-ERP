<%@ Page Title="" Language="C#" AutoEventWireup="true" 
    CodeFile="Approval_Group_Department.aspx.cs" Inherits="ASL_Libraries_Approval_Group_Department" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
</head>
<body style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;
    height: 100%;">
    <script language="javascript" type="text/javascript">

        function validation() {

            if (document.getElementById("txtApprovalGroup").value == "") {
                alert("Group Name is mandatory field.");
                document.getElementById("txtApprovalGroup").focus();
                return false;
            }
            return true
        }
        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127)) {
                event.returnValue = true;
            }
            else {
                event.returnValue = false;
                alert('Only numeric Value(0-9) is allowed.')
            }
        }
        
    </script>
    <form id="form1" runat="server">
    <div id="dvContent" style="text-align: center; border: 1px solid #5588BB;">
        <center>
            <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                height: 100%;">
                <div id="page-title" class="page-title">
                    Approval Group
                </div>
                <table border="0" cellpadding="2" cellspacing="2" width="100%">
                    <tr>
                        <td align="right" style="width: 20%">
                            Group Name :
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                            *
                        </td>
                        <td align="left" colspan="4" style="width: 35%">
                            <asp:TextBox ID="txtApprovalGroup" CssClass="txtInput" MaxLength="200" Width="400px" runat="server"></asp:TextBox>
                               <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" Display="None" ErrorMessage="Group name is mandatory field."
                                                    ControlToValidate="txtApprovalGroup" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 15%">
                            Department Type :
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                            *
                        </td>
                        <td align="left" colspan="4" style="width: 35%">
                            <asp:RadioButtonList ID="rdbDepartment" CssClass="txtInput" Width="400px" RepeatDirection="Horizontal"
                                runat="server" AutoPostBack="true" onselectedindexchanged="rdbDepartment_SelectedIndexChanged">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 15%">
                            Department :
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                        </td>
                        <td align="left" style="width: 35%">
                            <div style="float: left; text-align: left; width: 300px; height: 300px; overflow-x: hidden;
                                border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                background-color: #ffffff;">
                                <asp:CheckBoxList ID="chkDepartment" RepeatLayout="Flow" RepeatDirection="Vertical"
                                    runat="server">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                        <%--<td align="right" style="width: 20%">
                            User List :
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                        </td>
                        <td align="left" style="width: 35%">
                            <div style="float: left; text-align: left; width: 300px; height: 300px; overflow-x: hidden;
                                border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                background-color: #ffffff;">
                                <asp:CheckBoxList ID="chkUserList" RepeatLayout="Flow" RepeatDirection="Vertical"
                                    runat="server">
                                </asp:CheckBoxList>
                            </div>
                        </td>--%>
                    </tr>
                    <tr>
                        <td colspan="6" align="right" style="color: #FF0000; font-size: small;">
                            * Mandatory fields
                        </td>
                    </tr>
                </table>
                <div style="margin-top: 20px; background-color: #d8d8d8; text-align: center">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnDraft" runat="server" Text="Save" ValidationGroup="vgSubmit" Width="150px" OnClick="btnDraft_Click" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtGroupID" Visible="false" CssClass="txtInput" Width="1px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Label ID="lblMessage" Style="color: #FF0000;" runat="server"></asp:Label>
                     <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="vgSubmit" />
                </div>
            </div>
        </center>
    </div>
    </form>
</body>
</html>
