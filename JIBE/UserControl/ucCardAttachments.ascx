<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCardAttachments.ascx.cs"
    Inherits="UserControl_ucCardAttachments" %>

<style type="text/css">
    .attachments-popup
    {
        position: absolute;
        width: 250px;
        border: 1px solid #D0A9F5;
        color: Black;
        text-align: left;
        filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
        background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
        background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
        color: Black;
        padding:2px;
        font-size:11px;
        
    }
    .attachment-user-control
    {
        
    }
</style>
<asp:Panel ID="pnl_uc_main" CssClass="attachment-user-control" runat="server" Width="100px">
    <asp:ImageButton ID="ImgAttachments" runat="server" ImageUrl="~/Images/attach-icon.png" ImageAlign="AbsMiddle" OnClick="ImgAttachments_Click"/>
    <asp:HiddenField ID="hdnCardID" runat="server" Value="0"/>
    <asp:HiddenField ID="hdnUserID" runat="server" Value="0" />
</asp:Panel>
<asp:Panel ID="pnl_uc_popup" runat="server" CssClass="attachments-popup" Visible="false">
    <div style="text-align:right;font-weight:bold;"><div style="border:1px solid white;float:right;padding-left:2px;padding-right:2px;cursor:hand;"><asp:ImageButton ID="ImgClose" runat="server" ImageUrl="~/Images/cancel.png" ImageAlign="AbsMiddle" OnClick="ImgClose_Click"/></div></div>
    <div>
        <asp:Repeater runat="server" ID="rptAttachments">
            <HeaderTemplate>
                <table style='width: 100%;'>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Eval("AttachmentType")%>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <%# "<a href='../Uploads/CrewDocuments/" + Eval("ATTACHMENT_PATH").ToString() + "' target='_blank'>"%>
                        <%# Eval("ATTACHMENT_NAME").ToString() + "</a>" %>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Panel>
