<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_EditStatus.aspx.cs"
    Inherits="Crew_CrewDetails_EditStatus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../Styles/CrewDetails.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptmngPO" runat="server">
    </asp:ScriptManager>
    <div id="dvCrewLogGrid">
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:Panel ID="pnlCrewStatus" runat="server" Visible="false" Height="250px">
            <table style="width:100%" cellpadding="4">                
                <tr>
                    <td>
                        New Status
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCrewStatus" runat="server">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="INACTIVE"></asp:ListItem>
                            <asp:ListItem Text="Active" Value="ACTIVE"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="crewstatus"
                            runat="server" ControlToValidate="ddlCrewStatus" Display="Dynamic" InitialValue="0"
                            ErrorMessage="Please select status"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td >
                        Remark:
                    </td>
                    <td >
                        <asp:TextBox ID="txtCrewStatusChangeRemark" runat="server" TextMode="MultiLine" Height="60px"
                            Width="300px"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="red" ValidationGroup="crewstatus" runat="server"
                            ControlToValidate="txtCrewStatusChangeRemark" Display="Dynamic" ErrorMessage="Please enter remarks!!"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center">
                        <asp:Button ID="btnSaveStatus" runat="server" Text="Save" OnClick="btnSaveStatus_Click"
                            ValidationGroup="crewstatus"></asp:Button>
                        <input type="button" id="btnCloseStatus" value="Close" onclick="parent.hideModal('dvPopupFrame');" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
