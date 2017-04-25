<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPurc_Rollback_Reqsn.ascx.cs"
    Inherits="UserControl_ucPurc_Rollback_Reqsn" %>
<%@ Register Src="../UserControl/ucReqsncancelLog.ascx" TagName="ucReqsncancelLog"
    TagPrefix="uc1" %>
<table style="height: auto; width: 495px" cellpadding="0" cellspacing="0">
    <tr>
        <td style="vertical-align: top; border-bottom: 1px solid gray; padding-bottom: 5px">
            <table style="width: 495px" cellpadding="0" cellspacing="0">
                <tr align="center">
                    <td style="font-size: 12px; width: 100%; padding: 5px 0px 5px 0px; border-bottom: 1px solid gray;"
                        class="popup-css" colspan="2">
                        <asp:Label ID="Label1" runat="server" Text="Roll Back" Style="color: Black; font-weight: 700;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%; font-size: 12px; padding: 2px 0px 2px 0px; text-align: right">
                        Requisition No.:
                    </td>
                    <td style="width: 50%; font-size: 12px; text-align: left">
                        <asp:Label ID="lblReqsnNoRollBack" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="text-align: left; font-size: 12px; padding-bottom: 8px" >
           <asp:Label ID="lblPOdtl" runat="server" Text="Below PO(s) will be cancelled.&nbsp"></asp:Label>
            <table>
                <tr>
                    <td style="font-weight: bold; border-bottom: 1px solid gray">
                      <asp:Label ID="lblOrdCode" runat="server" Text="Order Code"></asp:Label> 
                    </td>
                    <td style="font-weight: bold; border-bottom: 1px solid gray">
                     <asp:Label ID="lblsupplName" runat="server" Text="Supplier Name"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="padding-right: 10px;">
                        <a id="alinkOrder" runat="server" target="_blank" style="color: Red"></a>
                    </td>
                    <td style="">
                        <asp:Label ID="lblsuppName" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="padding-top: 5px">
            <table style="width: 495px" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="font-size: 12px; text-align: right; padding-right: 5px">
                        Roll Back Stages :
                    </td>
                    <td align="left">
                        <asp:ListBox ID="DDLReqStages" runat="server" Style="font-size: small" Width="223px">
                        </asp:ListBox>
                    </td>
                    <td style="color: #FF0000; font-size: small;" align="left">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDDLReqStages" ValidationGroup="rollbk"
                            runat="server" ControlToValidate="DDLReqStages" ErrorMessage="Please select stage"
                            Display="None"></asp:RequiredFieldValidator>
                        <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtenderDDLReqStages" TargetControlID="RequiredFieldValidatorDDLReqStages"
                            runat="server">
                        </tlk4:ValidatorCalloutExtender>
                        *
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="height: 3px">
                    </td>
                </tr>
                <tr>
                    <td style="font-size: 12px; text-align: right; padding-right: 5px">
                        Reason:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtReason" runat="server" Height="57px" Style="font-size: small"
                            TextMode="MultiLine" Width="98%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtReason" ControlToValidate="txtReason"
                            ValidationGroup="rollbk" runat="server" Display="None" ErrorMessage="Please enter reason."></asp:RequiredFieldValidator>
                        <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtenderReason" TargetControlID="RequiredFieldValidatortxtReason"
                            runat="server">
                        </tlk4:ValidatorCalloutExtender>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="padding-top: 5px">
            <table style="width: 495px" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center">
                        <asp:Button ID="btndivReqprioOK" runat="server" Text="Ok" Font-Size="small" Width="100px"
                            ValidationGroup="rollbk" Height="30px" OnClick="btndivReqprioOK_Click" />
                        &nbsp;
                        <asp:Button ID="btndivReqPrioCancel" runat="server" Width="100" Height="30px" Text="Cancel"
                            Font-Size="small" OnClientClick="javascript:document.getElementById('divReqStages').style.display='none';try{ AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.hide();}catch(err){} ;return false; " />
                    </td>
                </tr>
                <tr>
                    <td style="color: #FF0000; font-size: small;" align="left">
                        <div style="max-height: 80px; overflow: auto">
                            <uc1:ucReqsncancelLog ID="ucReqsncancelLog1" runat="server" />
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

