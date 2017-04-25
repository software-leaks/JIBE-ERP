<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPurcQuotationApproval.ascx.cs"
    Inherits="UserControl_ucPurcQuotationApproval" %>
<table cellpadding="2" width="500px" cellspacing="0">
    <tr>
        <td style="font-size: 10px; font-weight: bold; text-align: center; font-family: Verdana"
            colspan="2">
            Send For Approval
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center" style="padding: 2px 0px 2px 0px; font-family: Verdana">
            <asp:Label ID="lblreqsnCode" Font-Size="11px" runat="server"> </asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 23%; text-align: right; font-size: 11px; font-weight: bold; border-top: 1px solid gray;
            padding-top: 8px; font-family: Verdana">
            Select Approver :
        </td>
        <td style="text-align: left; width: 77%; border-top: 1px solid gray; padding-top: 8px">
            <asp:ListBox ID="lstUserList" runat="server" DataTextField="UserName" DataValueField="UserID"
                Height="150px" ValidationGroup="apr" Width="99%" Font-Size="12px" Font-Names="verdana">
            </asp:ListBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ValidationGroup="apr"
                runat="server" ControlToValidate="lstUserList" InitialValue="0" ErrorMessage="Please select approver !"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td style="width: 23%; text-align: right; font-size: 11px; font-family: Verdana;
            font-weight: bold">
            Remark :
        </td>
        <td style="width: 77%">
            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="99%" Height="60px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center; padding-top: 5px">
            <asp:Button ID="btnSendForApproval" runat="server" Height="30px" Text="Save & Send For Approval"
                ValidationGroup="apr" OnClick="btnSendForApproval_Click" />
            <asp:Button ID="btnSendForApprovalCancel" Height="30px" runat="server" Text="Close" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdfReqsnCode" runat="server" />
<asp:HiddenField ID="hdfDocumentCode" runat="server" />
<asp:HiddenField ID="hdfVesselCode" runat="server" />
<asp:HiddenField ID="hdfCallstsSaved" Value="1" runat="server" />
