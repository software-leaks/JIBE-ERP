<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlRecordNavigation.ascx.cs" 
    Inherits="UserControl_ctlRecordNavigation" %>
<asp:Panel ID="pnlNavigation" runat="server">
    <table >
        <tr>
            <td>
                <asp:Button ID="btnMoveFirst" style="background:url('/JIBE/images/Move_First.png');padding:0px;height:28px;width:36px" BorderWidth="0px" runat="server" 
                    onclick="btnMoveFirst_Click" />
            </td>
            <td>
                <asp:Button ID="btnMovePrev" runat="server"  style="background:url('/JIBE/images/Move_Prev.png');padding:0px;height:28px;width:36px" BorderWidth="0px" 
                    onclick="btnMovePrev_Click" />
            </td>
            <td>
                <asp:Label ID="lblCurrentPosition" runat="server" Text="" Font-Bold="true" Font-Size="11px" Font-Names="Verdana" CssClass="data" />
            </td>
            <td>
                <asp:Button ID="btnMoveNext" runat="server"  style="background:url('/JIBE/images/Move_Next.png');padding:0px;height:28px;width:36px" BorderWidth="0px" 
                    onclick="btnMoveNext_Click" />
            </td>
            <td>
                <asp:Button ID="btnMoveLast" runat="server"  style="background:url('/JIBE/images/Move_Last.png');padding:0px;height:28px;width:36px" BorderWidth="0px" 
                    onclick="btnMoveLast_Click" />
            </td>
        </tr>
    </table>

    <asp:HiddenField ID="hdfCurrentID" Value="0" runat="server" />
     <asp:HiddenField ID="hdfRecordCount" Value="0" runat="server" />
      <asp:HiddenField ID="hdfCurrentIndex" Value="0" runat="server" />

</asp:Panel>
