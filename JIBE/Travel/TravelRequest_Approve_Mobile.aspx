<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TravelRequest_Approve_Mobile.aspx.cs"
    Inherits="Travel_TravelRequest_Approve_Mobile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>JIBE::Approve Travel Request</title>
    
</head>
<body>
    <form id="form1" runat="server" style="font-size: 35px; font-family: Tahoma">
    <div style="width: 100%; border: 1px solid gray">
        <table width="99%" cellspacing="0" cellpadding="1">
            <tr>
                <td>
                    <asp:Label ID="lblVesselName" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPaxName" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblRank" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gvQuotations" runat="server" AutoGenerateColumns="false" GridLines="None"
                        CellSpacing="1" HeaderStyle-BackColor="Orange" RowStyle-BackColor="AliceBlue"
                        Width="100%" OnRowDataBound="gvQuotations_RowDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="Agent" DataField="AgentName" />
                            <asp:BoundField HeaderText="Amount" DataField="Amount" ItemStyle-HorizontalAlign="Right" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button ID="btnApprove" runat="server" CommandArgument='<% Eval("QuoteID")%>'
                                        Text="Approve" Font-Size="30px" OnClick="btnApprove_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
