<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPurcReqsnHold_UnHold.ascx.cs"
    Inherits="UserControl_ucPurcReqsnHold_UnHold" %>
<link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
<table border="0" style=" vertical-align: top;" class="popup-css" cellpadding="0" cellspacing="0">
    <tr>
        <td style="vertical-align: top">
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr align="center">
                    <td style="font-size: small; width: 90%; text-align: center; border-bottom: 1px solid gray">
                        <asp:Label ID="lblUrgencyTitle" runat="server" Text="Requisition On Hold" Style="color: Black;
                            font-weight: 700; font-size: small;"></asp:Label>
                    </td>
                    <td align="right" style="width: 10%; text-align: right; border-bottom: 1px solid gray">
                        <asp:ImageButton ID="ImageButton1" OnClick="btndivCancel_Click" ImageUrl="~/purchase/Image/Close.gif"
                            runat="server" Style="font-size: small" Width="12px" Height="12px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <div style="max-height: 80px; width: 495px; overflow: auto">
                            <asp:GridView ID="rgdHoldLog" runat="server" Width="490" showstatusbar="True" Font-Size="8px"
                                Font-Names="verdana" AutoGenerateColumns="False" AllowSorting="false" AllowPaging="false"
                                GridLines="None" EmptyDataText="No  logs found." CellSpacing="0" CellPadding="0">
                                <Columns>
                                    <asp:BoundField HeaderText="User" ItemStyle-Width="80px" DataField="User_name" HeaderStyle-BackColor="#D3D3A8"
                                        HeaderStyle-Font-Names="Verdana" HeaderStyle-Font-Size="11px" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Font-Size="9px" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Date" ItemStyle-Width="100px" DataField="OnHoldDate"
                                        HeaderStyle-BackColor="#D3D3A8" HeaderStyle-Font-Names="Verdana" HeaderStyle-Font-Size="11px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Font-Size="9px" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Action" ItemStyle-Width="90px" DataField="OnHold" HeaderStyle-BackColor="#D3D3A8"
                                        HeaderStyle-Font-Names="Verdana" HeaderStyle-Font-Size="11px" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Font-Size="9px" ItemStyle-HorizontalAlign="Left" />
                                </Columns>
                                <AlternatingRowStyle BackColor="ActiveBorder" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top">
            <table style="width: 495px" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="font-size: 12px; font-family: Verdana">
                        Remarks :
                    </td>
                    <td>
                        <asp:TextBox ID="txtRemarks" runat="server" ValidationGroup="remark" TextMode="MultiLine"
                            Width="430px" Height="70" MaxLength="199"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqValtxtRemarksHold" runat="server" ValidationGroup="remark"
                            Display="None" ControlToValidate="txtRemarks" ErrorMessage="Please enter remark"></asp:RequiredFieldValidator>
                        <tlk4:ValidatorCalloutExtender TargetControlID="ReqValtxtRemarksHold" ID="ValidatorCalloutExtenderHold"
                            runat="server">
                        </tlk4:ValidatorCalloutExtender>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top">
            <table style="width: 495px; text-align: center" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Button ID="btndivSave" runat="server" ValidationGroup="remark" Text="Save" Height="30px"
                            Font-Size="12px" Width="100px" OnClick="btndivSave_Click" CssClass="ml" />
                    </td>
                    <td>
                        <asp:Button ID="btndivCancel" runat="server" Text="Cancel" Height="30px" Font-Size="12px"
                            Width="100px" OnClick="btndivCancel_Click" CssClass="mr" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="color: #FF0000; font-size: small;" align="left">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
